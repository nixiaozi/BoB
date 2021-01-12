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

        public bool UpdateTheTaskStatus(Guid taskID, TaskExecuteStatusEnum taskExecuteStatus, MaindbContext context = null)
        {
            AllTasks data = new AllTasks { ID = taskID };
            return Update(context==null?new MaindbContext():context, data, s =>
            {
                s.TaskExecuteStatus = taskExecuteStatus;
                return s;
            });

        }

        public bool DeleteTheTask(Guid taskID, MaindbContext context = null)
        {
            return Delete(context==null?new MaindbContext(): context, taskID);
        }

        public IQueryable<AllTasks> GetAllTasks(MaindbContext context)
        {
            return GetList(context, s => s.Status == BoB.EFDbContext.Enums.DataStatus.Normal);
        }
    }
}
