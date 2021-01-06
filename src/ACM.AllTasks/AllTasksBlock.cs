using ACM.BaseAutoAction;
using ACM.MainDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACM.AllTasksEntities
{
    public class AllTasksBlock:BaseBlock<AllTasks,Guid>,IBaseBlock<AllTasks,Guid>,IAllTasksBlock
    {
        public bool AddNewTask(AllTasks theTask)
        {
            return Insert(new MaindbContext(), theTask);
        }

        public bool UpdateTheTaskStatus(Guid taskID, TaskExecuteStatusEnum taskExecuteStatus)
        {
            AllTasks data = new AllTasks { ID = taskID };
            return Update(new MaindbContext(), data, s =>
            {
                s.TaskExecuteStatus = taskExecuteStatus;
                return s;
            });

        }

        public bool DeleteTheTask(Guid taskID)
        {
            return Delete(new MaindbContext(), taskID);
        }

        public IQueryable<AllTasks> GetAllTasks(MaindbContext context)
        {
            return GetList(context, s => s.Status == BoB.EFDbContext.Enums.DataStatus.Normal);
        }
    }
}
