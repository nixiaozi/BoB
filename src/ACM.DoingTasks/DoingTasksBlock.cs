using ACM.BaseAutoAction;
using ACM.MainDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACM.DoingTasksEntities
{
    public class DoingTasksBlock:BaseBlock<DoingTasks,Guid>,IBaseBlock<DoingTasks,Guid>,IDoingTasksBlock
    {
        public bool AddNewDoingTask(DoingTasks task)
        {
            return Insert(new MaindbContext(), task);
        }
        public bool UpdateDoingTaskStatus(Guid TaskID, DoingTaskStatusEnum doingTaskStatus)
        {
            DoingTasks data = new DoingTasks { TaskID = TaskID };
            return Update(new MaindbContext(), data, s =>
            {
                s.TaskExecingStatus = doingTaskStatus;
                return s;
            });
        }

        public bool RemoveDoingTask(Guid TaskID)
        {
            using(var context= new MaindbContext())
            {
                var task = Get(context, s => s.TaskID == TaskID);
                return Remove(context, task.ID);
            }
        }

        public IQueryable<DoingTasks> GetAllDoingTasks(MaindbContext context)
        {
            return GetList(context, s => s.Status == BoB.EFDbContext.Enums.DataStatus.Normal);
        }
    }
}
