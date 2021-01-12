using ACM.BaseAutoAction;
using ACM.MainDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACM.DoingTasksEntities
{
    public class DoingTasksBlock:BaseBlock<DoingTasks,int>,IBaseBlock<DoingTasks, int>,IDoingTasksBlock
    {
        public bool AddNewDoingTask(DoingTasks task)
        {
            if(Get(s=>s.TaskID==task.TaskID)==null)
                return Insert(new MaindbContext(), task);

            return true;
        }
        public bool UpdateDoingTaskStatus(Guid TaskID, DoingTaskStatusEnum doingTaskStatus,MaindbContext context)
        {
            DoingTasks data = new DoingTasks { ID = Get(context, s => s.TaskID == TaskID).ID };
            return Update(context, data, s =>
            {
                s.TaskExecingStatus = doingTaskStatus;
                return s;
            });
                
        }

        public bool RemoveDoingTask(Guid TaskID, MaindbContext context)
        {
            //using(context==null? new MaindbContext():context) // update 方法不要在这个级别使用using 
            //{
                var task = Get(context, s => s.TaskID == TaskID);
                return Remove(context, task.ID);
            //}
        }

        public IQueryable<DoingTasks> GetAllDoingTasks(MaindbContext context)
        {
            return GetList(context, s => s.Status == BoB.EFDbContext.Enums.DataStatus.Normal);
        }
    }
}
