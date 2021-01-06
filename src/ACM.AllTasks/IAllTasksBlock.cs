using ACM.BaseAutoAction;
using ACM.MainDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACM.AllTasksEntities
{
    public interface IAllTasksBlock
    {
        public bool AddNewTask(AllTasks theTask);

        public bool UpdateTheTaskStatus(Guid taskID, TaskExecuteStatusEnum taskExecuteStatus);

        public bool DeleteTheTask(Guid taskID);

        public IQueryable<AllTasks> GetAllTasks(MaindbContext context);
    }
}
