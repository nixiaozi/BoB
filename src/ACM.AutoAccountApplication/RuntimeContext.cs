using ACM.AllTasksEntities;
using ACM.AppListEntities;
using ACM.BaseAutoAction;
using ACM.DoingTasksEntities;
using ACM.TaskManager;
using ACM.TaskManager.Model;
using Autofac;
using BoB.ContainManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;
using Newtonsoft.Json;
using System.Threading;

namespace ACM.AutoAccountApplication
{
    public class RuntimeContext
    {
        private RuntimeContext() { }

        private static RuntimeContext _runtimeContext;
        private static List<AppList> AllAppList;

        private static ITaskManagerService _taskManagerService;
        private static IEnumerable<AutoActionAdapter> _autoActionAdapters;
        // 最好使用Manager 进行统一的管理，已确保所有的Context都完成，并且所有task表同步
        //private static IAllTasksBlock _allTasksBlock;
        //private static IDoingTasksBlock _doingTasksBlock;
        private static IAppListBlock _appListBlock;
        static RuntimeContext()
        {
            _runtimeContext = new RuntimeContext(); // 单例模式，对象成员赋值

            
            // Ioc初始化
            _taskManagerService = BoBContainer.ServiceContainer.Resolve<ITaskManagerService>();
            _autoActionAdapters= BoBContainer.ServiceContainer.Resolve<IEnumerable<AutoActionAdapter>>();

            //_allTasksBlock = BoBContainer.ServiceContainer.Resolve<IAllTasksBlock>();
            //_doingTasksBlock = BoBContainer.ServiceContainer.Resolve<IDoingTasksBlock>();
            _appListBlock = BoBContainer.ServiceContainer.Resolve<IAppListBlock>();


            // 数据初始化
            AllAppList = _appListBlock.GetAllTheApps();
        }
        
        public static RuntimeContext Instance { get { return _runtimeContext; } }




        /* 定义单例模式之后，通过单例模式获取使用中用户列表  */
        private static List<CancellableTask<TaskDetailOutput>> doingTasks; // 只需要有一个

        public List<CancellableTask<TaskDetailOutput>> DoingTasks
        {
            get
            {
                if (RuntimeContext.doingTasks == null)
                {
                    RuntimeContext.doingTasks = new List<CancellableTask<TaskDetailOutput>>();
                }
                return RuntimeContext.doingTasks;
            }
        }

        public List<CancellableTask<TaskDetailOutput>> AddDoingTask(CancellableTask<TaskDetailOutput> cancellableTask)
        {
            if (doingTasks == null)
                doingTasks = new List<CancellableTask<TaskDetailOutput>>();

            doingTasks.Add(cancellableTask);

            return DoingTasks;
        }

        public List<CancellableTask<TaskDetailOutput>> RemoveDoingTask(CancellableTask<TaskDetailOutput> item)
        {
            if(doingTasks!=null && doingTasks.Count > 0)
            {
                doingTasks.Remove(item);
            }
            return DoingTasks;
        }

        // 处理已经在执行的任务
        public void HandlerDoingTaskAction()
        {
            if (DoingTasks.Count == 0)
            {
                // 等于零时不需要任何操作，直接进入到下一步，扫描并添加新任务
                return;
            }
            else
            {

                var resultDoingTasks =new List<CancellableTask<TaskDetailOutput>>();
                resultDoingTasks.AddRange(DoingTasks);
                //扫描所有的任务，对于已经终止的任务进行完成操作
                foreach (var taskDetail in resultDoingTasks)
                {
                    _taskManagerService.ChangeTaskDetailStatus(taskDetail.TaskDetail.TaskID, taskDetail.TheTask.Status,taskDetail.CreateTime,()=>
                    {
                        if(taskDetail.TheTask.Status== TaskStatus.RanToCompletion|| taskDetail.TheTask.Status == TaskStatus.Faulted
                            || taskDetail.TheTask.Status == TaskStatus.Canceled)
                        {
                            RemoveDoingTask(taskDetail);
                        }
                        else if(taskDetail.TheTask.Status == TaskStatus.Running 
                            && DateTime.Now - taskDetail.CreateTime >new TimeSpan(0,BoBConfiguration.AutoCancelMinutes,0))
                        {
                            taskDetail.CancelTask();
                        }
                    });
                }


            }
        }

        private List<TaskDetailOutput> PrepareNewTask()
        {
            // 首先Prepare Very High
            var AddDoingTasks = _taskManagerService.GetAllUndoTasksByTaskLevel(ACMTaskLevelEnum.VeryHigh);


            // 然后按顺序Prepare 其他等级的数据
            if (BoBConfiguration.NormalAllowParallelTaskNum > AddDoingTasks.Count + DoingTasks.Count)
            {
                AddDoingTasks.AddRange(_taskManagerService.GetAllUndoTasksByTaskLevel(ACMTaskLevelEnum.Heigh));
            }

            if (BoBConfiguration.NormalAllowParallelTaskNum > AddDoingTasks.Count + DoingTasks.Count)
            {
                AddDoingTasks.AddRange(_taskManagerService.GetAllUndoTasksByTaskLevel(ACMTaskLevelEnum.Normal));
            }

            if (BoBConfiguration.NormalAllowParallelTaskNum > AddDoingTasks.Count + DoingTasks.Count)
            {
                AddDoingTasks.AddRange(_taskManagerService.GetAllUndoTasksByTaskLevel(ACMTaskLevelEnum.Low));
            }

            foreach(var item in AddDoingTasks)
            {
                _taskManagerService.PrepareTheTask(new DoingTasksEntities.DoingTasks
                {
                    ParamObj = item.TaskParams,
                    Status = BoB.EFDbContext.Enums.DataStatus.Normal,
                    TaskExecingStatus = DoingTaskStatusEnum.Prepare,
                    TaskID = item.TaskID,
                    TaskType = item.TaskType,
                });
            }

            return AddDoingTasks;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ExtendTasks">额外的任务条目</param>
        /// <param name="IsUnionBase">是否合并默认，默认不合并</param>
        public void DoingPrepareTask(List<TaskDetailOutput> ExtendTasks=null,bool IsUnionBase=false)
        {
            var TheAddDoingTasks = IsUnionBase ? PrepareNewTask() : new List<TaskDetailOutput>();
            if (ExtendTasks != null)
            {
                TheAddDoingTasks.AddRange(ExtendTasks);
            }

            foreach (var item in TheAddDoingTasks)
            {
                

                _taskManagerService.DoingPrepareTask(item.TaskID, () =>
                {
                    var newTask = new CancellableTask<TaskDetailOutput>(item, (t, ct) =>
                    {
                        TaskTypeAuto(t, ct);
                    });
                    AddDoingTask(newTask); // 添加到列表中
                    newTask.DoTask(); // 直接开始任务
                });
            }


        }


        public void TaskTypeAuto(TaskDetailOutput taskDetail,CancellationToken ct)
        {
            var appIdentity = AllAppList.FirstOrDefault(s => s.ID == taskDetail.AppID).IdentityName;
            AutoActionAdapter adapter = _autoActionAdapters.FirstOrDefault(s =>
                                      s.CommandText == appIdentity);

            switch (taskDetail.TaskType)
            {
                case ACMTaskTypeEnum.Attention:
                    adapter.DoBrowserToAttention(taskDetail.UserID, JsonConvert.DeserializeObject<AttentionAction>(taskDetail.TaskParams), ct);
                    break;
                case ACMTaskTypeEnum.Barrage:
                    adapter.DoBrowserToBarrage(taskDetail.UserID, JsonConvert.DeserializeObject<BarrageAction>(taskDetail.TaskParams), ct);
                    break;
                case ACMTaskTypeEnum.Collect:
                    adapter.DoBrowserToCollect(taskDetail.UserID, JsonConvert.DeserializeObject<CollectAction>(taskDetail.TaskParams), ct);
                    break;
                case ACMTaskTypeEnum.Comment:
                    adapter.DoBrowserToComment(taskDetail.UserID, JsonConvert.DeserializeObject<CommentAction>(taskDetail.TaskParams), ct);
                    break;
                case ACMTaskTypeEnum.GiveLike:
                    adapter.DoBrowserToGiveLike(taskDetail.UserID, JsonConvert.DeserializeObject<GiveLikeAction>(taskDetail.TaskParams), ct);
                    break;
                case ACMTaskTypeEnum.GiveReward:
                    // 暂时未定义
                    // adapter.DoBrowserToBarrage(JsonConvert.DeserializeObject<>(t.TaskParams), ct);
                    break;
                case ACMTaskTypeEnum.Login:
                    adapter.DoBrowserToLogin(taskDetail.UserID, JsonConvert.DeserializeObject<LoginAction>(taskDetail.TaskParams), ct);
                    break;
                case ACMTaskTypeEnum.RandomBrowse:
                    adapter.DoBrowserRandom(taskDetail.UserID, JsonConvert.DeserializeObject<RandomBrowse>(taskDetail.TaskParams), ct);
                    break;
                case ACMTaskTypeEnum.Share:
                    adapter.DoBrowserToShare(taskDetail.UserID, JsonConvert.DeserializeObject<ShareAction>(taskDetail.TaskParams), ct);
                    break;
                case ACMTaskTypeEnum.View:
                    adapter.DoBrowserToView(taskDetail.UserID, JsonConvert.DeserializeObject<ViewAction>(taskDetail.TaskParams), ct);
                    break;
            }

        }

        // 这个方法用于在程序启动时初始化上次未执行完成的任务
        public void InitTaskBefore()
        {
            // 添加方法对于已经在执行中但是实际执行完成的条目进行修复处理
            _taskManagerService.FixHasDoneError();


            var beforeTasks= _taskManagerService.GetBeforeTasks();

            var beforeDoingTasks = beforeTasks.Where(s => s.DoingTaskStatus == DoingTaskStatusEnum.Doing).ToList();

            var beforePerpareTasks = beforeTasks.Where(s => s.DoingTaskStatus == DoingTaskStatusEnum.Prepare).ToList();

            // 处理以前正在执行的任务
            foreach (var task in beforeDoingTasks)
            {
                var newTask = new CancellableTask<TaskDetailOutput>(task, (t, ct) =>
                {
                    TaskTypeAuto(t, ct);
                });
                AddDoingTask(newTask); // 添加到列表中
                newTask.DoTask(); // 直接开始任务
            }


            // 处理已经准备的任务
            DoingPrepareTask(beforePerpareTasks);


        }



    }
}
