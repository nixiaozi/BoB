using ACM.BaseAutoAction;
using ACM.MainDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACM.DoingTasksEntities
{
    public interface IDoingTasksBlock
    {
        public bool AddNewDoingTask(DoingTasks task);

        public bool UpdateDoingTaskStatus(Guid TaskID, DoingTaskStatusEnum doingTaskStatus);

        public bool RemoveDoingTask(Guid TaskID);

        public IQueryable<DoingTasks> GetAllDoingTasks(MaindbContext context);
    }
}
