using ACM.BaseAutoAction;
using ACM.MainDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACM.DoingTasksEntities
{
    public interface IDoingTasksBlock: IBaseBlock<DoingTasks, int>
    {
        public bool AddNewDoingTask(DoingTasks task);

        public bool UpdateDoingTaskStatus(Guid TaskID, DoingTaskStatusEnum doingTaskStatus,MaindbContext context);

        public bool RemoveDoingTask(Guid TaskID, MaindbContext context);

        public IQueryable<DoingTasks> GetAllDoingTasks(MaindbContext context);
    }
}
