using ACM.AllTasksEntities;
using ACM.BaseAutoAction;
using ACM.DoingTasksEntities;
using ACM.MainDatabase;
using ACM.TaskManager.Model;
using Autofac;
using BoB.ContainManager;
using BoB.EFDbContext.Enums;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACM.TaskManager
{
    public class TaskManagerService : InitBlockService, ITaskManagerService
    {
        private IAllTasksBlock _allTasksBlock;
        private IDoingTasksBlock _doingTasksBlock;

        protected override void Init()
        {
            _allTasksBlock = CurrentServiceContainer.Resolve<IAllTasksBlock>();
            _doingTasksBlock = CurrentServiceContainer.Resolve<IDoingTasksBlock>();
        }


        public List<TaskDetailOutput> GetDoingTaskDetail(MaindbContext context = null)
        {
            if (context == null) { context = new MaindbContext(); }
            using(context)
            {
                var allTasksQuery = _allTasksBlock.GetAllTasks(context);
                var doingTaskQuery = _doingTasksBlock.GetAllDoingTasks(context);

                var result =allTasksQuery.Join<AllTasks, DoingTasksEntities.DoingTasks, Guid, TaskDetailOutput>(
                    doingTaskQuery, x => x.ID, y => y.TaskID, (x, y) => new TaskDetailOutput
                    {
                        CreateDate = x.CreateTime,
                        DoingTaskStatus = y.TaskExecingStatus,
                        TaskExecuteStatus = x.TaskExecuteStatus,
                        TaskID = x.ID,
                        TaskLevel = x.TaskLevel,
                        TaskParams = x.ParamObj,
                        TaskType = x.TaskType,
                        AppID=x.AppID,
                        UserID=x.UserID,
                    }).ToList();

                return result;

            }
        }

        public List<TaskDetailOutput> GetUndoneTaskDetail(MaindbContext context = null)
        {
            List<AllTasks> allTasksList = null;
            List<DoingTasks> doingTaskList = null;
            if (context == null) { context = new MaindbContext(); }
            using (context)
            {
                allTasksList = _allTasksBlock.GetAllTasks(context).ToList();
                doingTaskList = _doingTasksBlock.GetAllDoingTasks(context).ToList();
            }

            var result = allTasksList.Select(s => new TaskDetailOutput
            {
                CreateDate = s.CreateTime,
                DoingTaskStatus = doingTaskList.FirstOrDefault(x => x.TaskID == s.ID)?.TaskExecingStatus,
                TaskExecuteStatus = s.TaskExecuteStatus,
                TaskID = s.ID,
                TaskLevel = s.TaskLevel,
                TaskParams = s.ParamObj,
                TaskType = s.TaskType,
                AppID=s.AppID,
                UserID=s.UserID,
            }).OrderByDescending(s=>s.CreateDate).ToList();

            return result;


        }


        public bool ChangeTaskDetailStatus(Guid TaskID, TaskStatus taskStatus,DateTime taskStartTime,Action tranAction = null)
        {
            switch (taskStatus)
            {
                case TaskStatus.RanToCompletion:
                    using(var context=new MaindbContext())
                    {
                        var transaction = context.Database.BeginTransaction();
                        var isDoDel= _doingTasksBlock.RemoveDoingTask(TaskID,context);
                        if (isDoDel)
                            _allTasksBlock.DoneTheTask(TaskID, BaseAutoAction.TaskExecuteStatusEnum.Completed, taskStartTime, context);

                        if (tranAction != null)
                        {
                            tranAction.Invoke();
                        }
                        transaction.Commit();
                    }
                    break;
                case TaskStatus.Faulted:
                    using (var context = new MaindbContext())
                    {
                        var transaction = context.Database.BeginTransaction();
                        var isDoDel = _doingTasksBlock.RemoveDoingTask(TaskID, context);
                        if (isDoDel)
                            _allTasksBlock.DoneTheTask(TaskID, BaseAutoAction.TaskExecuteStatusEnum.Fail, taskStartTime, context);
                        if (tranAction != null)
                        {
                            tranAction.Invoke();
                        }
                        transaction.Commit();
                    }
                    break;
                case TaskStatus.Canceled:
                    using (var context = new MaindbContext())
                    {
                        var transaction = context.Database.BeginTransaction();
                        var isDoDel = _doingTasksBlock.RemoveDoingTask(TaskID, context);
                        if (isDoDel)
                            _allTasksBlock.DoneTheTask(TaskID, BaseAutoAction.TaskExecuteStatusEnum.SystemClosure, taskStartTime, context);

                        if (tranAction != null)
                        {
                            tranAction.Invoke();
                        }
                        transaction.Commit();
                    }
                    break;
                case TaskStatus.Running:
                    using (var context = new MaindbContext())
                    {
                        var transaction = context.Database.BeginTransaction();
                        // 运行中不能删除任务执行表条目！！！
                        //var isDoDel = _doingTasksBlock.RemoveDoingTask(TaskID, context);
                        //if (isDoDel)
                        try
                        {
                            _allTasksBlock.DoneTheTask(TaskID, BaseAutoAction.TaskExecuteStatusEnum.Executing, taskStartTime, context);
                        }catch(Exception ex)
                        {

                        }

                        if (tranAction != null)
                        {
                            tranAction.Invoke();
                        }
                        transaction.Commit();
                    }
                    break;
                default:
                    using (var context = new MaindbContext())
                    {
                        var transaction = context.Database.BeginTransaction();
                        _allTasksBlock.UpdateTheTaskStatus(TaskID, BaseAutoAction.TaskExecuteStatusEnum.UnDo, context);
                        if (tranAction != null)
                        {
                            tranAction.Invoke();
                        }
                        // _doingTasksBlock.RemoveDoingTask(TaskID, context); 在未开始情况下不需要执行此操作
                        transaction.Commit();
                    }
                    break;

            }

            return true;

        }


        public List<TaskDetailOutput> GetAllUndoTasksByTaskLevel(ACMTaskLevelEnum TaskLevel)
        {
            List<AllTasks> allTasksList = null;
            allTasksList = _allTasksBlock.AsyncGetList( s => s.Status == DataStatus.Normal && s.TaskExecuteStatus == TaskExecuteStatusEnum.UnDo
                     && s.TaskLevel == TaskLevel);

            var result = allTasksList.Select(s => new TaskDetailOutput
            {
                CreateDate = s.CreateTime,
                DoingTaskStatus = DoingTaskStatusEnum.Prepare,
                TaskExecuteStatus = s.TaskExecuteStatus,
                TaskID = s.ID,
                TaskLevel = s.TaskLevel,
                TaskParams = s.ParamObj,
                TaskType = s.TaskType,
                AppID=s.AppID,
                UserID=s.UserID,
            }).ToList();

            return result;

        }

        public bool PrepareTheTask(DoingTasks theTask)
        {
            using(var context =new MaindbContext())
            {
                return _doingTasksBlock.Insert(context, theTask);
            }


        }


        public bool DoingPrepareTask(Guid TaskID, Action tranAction = null)
        {
            using(var context =new MaindbContext())
            {
                var transaction = context.Database.BeginTransaction();
                _allTasksBlock.UpdateTheTaskStatus(TaskID, TaskExecuteStatusEnum.Executing, context);
                _doingTasksBlock.UpdateDoingTaskStatus(TaskID, DoingTaskStatusEnum.Doing, context);
                if (tranAction != null)
                {
                    tranAction.Invoke();
                }
                transaction.Commit(); // 可能出现transaction已完成的错误提示，说明前面使用context时使用了using 造成了context自动完成

                return true;
            }

        }


        public List<TaskDetailOutput> GetBeforeTasks()
        {
            List<TaskDetailOutput> result = new List<TaskDetailOutput>();

            using(var context =new MaindbContext())
            {
                var  allTasksQuery = _allTasksBlock.GetAllTasks(context);
                var doingTasksQuery = _doingTasksBlock.GetAllDoingTasks(context);

                result = doingTasksQuery.Join(allTasksQuery, x => x.TaskID, y => y.ID, (x,y) => new TaskDetailOutput
                {
                    AppID=y.AppID,
                    CreateDate=y.CreateTime,
                    DoingTaskStatus=x.TaskExecingStatus,
                    TaskExecuteStatus=y.TaskExecuteStatus,
                    TaskID=x.TaskID,
                    TaskLevel=y.TaskLevel,
                    TaskParams=y.ParamObj,
                    TaskType=y.TaskType,
                    UserID=y.UserID,
                }).ToList();

            }



            return result;
        }



        public void FixHasDoneError()
        {
            List<AllTasks> data;

            using (var context = new MaindbContext())
            {
                var allDoingTaskQuery = _allTasksBlock.GetList(context, s => s.TaskExecuteStatus == TaskExecuteStatusEnum.Executing);
                var realDoingTaskQuery = _doingTasksBlock.GetAllDoingTasks(context).Select(s=>s.TaskID);

                data = allDoingTaskQuery.Where(x => !realDoingTaskQuery.Contains(x.ID)).ToList();

                foreach(var item in data)
                {
                    item.TaskExecuteStatus = TaskExecuteStatusEnum.SystemClosure;
                }

                context.SaveChanges();
            }

        }

    }
}
