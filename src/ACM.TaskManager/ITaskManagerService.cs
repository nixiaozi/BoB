using ACM.BaseAutoAction;
using ACM.DoingTasksEntities;
using ACM.MainDatabase;
using ACM.TaskManager.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ACM.TaskManager
{
    public interface ITaskManagerService
    {
        public List<TaskDetailOutput> GetDoingTaskDetail(MaindbContext context = null);


        public List<TaskDetailOutput> GetUndoneTaskDetail(MaindbContext context = null);


        public bool ChangeTaskDetailStatus(Guid TaskID, TaskStatus taskStatus, DateTime taskStartTime, Action tranAction=null);


        public List<TaskDetailOutput> GetAllUndoTasksByTaskLevel(ACMTaskLevelEnum TaskLevel);

        public bool PrepareTheTask(DoingTasks theTask);

        public bool DoingPrepareTask(Guid TaskID, Action tranAction = null);

        public List<TaskDetailOutput> GetBeforeTasks();

    }
}
