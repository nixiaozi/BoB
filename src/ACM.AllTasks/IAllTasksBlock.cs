using ACM.BaseAutoAction;
using ACM.MainDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACM.AllTasksEntities
{
    public interface IAllTasksBlock: IBaseBlock<AllTasks, Guid>
    {
        public bool AddNewTask(AllTasks theTask);

        public bool UpdateTheTaskStatus(Guid taskID, TaskExecuteStatusEnum taskExecuteStatus, MaindbContext context=null);

        public bool DeleteTheTask(Guid taskID, MaindbContext context = null);

        public IQueryable<AllTasks> GetAllTasks(MaindbContext context);


        public bool DoneTheTask(Guid taskID, TaskExecuteStatusEnum taskExecuteStatus, DateTime startTime, MaindbContext context);

    }
}
