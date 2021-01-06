using ACM.AllTasksEntities;
using ACM.DoingTasksEntities;
using ACM.MainDatabase;
using ACM.TaskManager.Model;
using BoB.ContainManager;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACM.TaskManager
{
    public class TaskManagerService : InitBlockService, ITaskManagerService
    {
        private IAllTasksBlock _allTasksBlock;
        private IDoingTasksBlock _doingTasksBlock;

        protected override void Init()
        {
            _allTasksBlock = CurrentServiceProvider.GetService<IAllTasksBlock>();
            _doingTasksBlock = CurrentServiceProvider.GetService<IDoingTasksBlock>();
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
                        TaskType = x.TaskType
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
            }).ToList();

            return result;


        }

    }
}
