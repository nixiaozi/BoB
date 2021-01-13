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
        public  override bool Insert(MaindbContext context, DoingTasks data)
        {
            if (context == null)
            {
                throw new Exception("MaindbContext params Cant be null!");
            }

            if(Get(s => s.TaskID == data.TaskID) == null)
            {
                context.Add<DoingTasks>(data);
                context.SaveChanges();
            }
            return true;
        }

        // Insert 方法都默认不会使用using 自动关闭 context，单独使用需要自行定义
        public bool AddNewDoingTask(DoingTasks task)
        {
            if (Get(s => s.TaskID == task.TaskID) == null)
            {
                using (var context = new MaindbContext())
                {
                    return Insert(context, task);
                }
            }

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

        /// <summary>
        /// 返回false表示并没有执行删除操作，可能已删除
        /// </summary>
        /// <param name="TaskID"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public bool RemoveDoingTask(Guid TaskID, MaindbContext context)
        {
            //using(context==null? new MaindbContext():context) // update 方法不要在这个级别使用using 
            //{
            var task = Get(context, s => s.TaskID == TaskID);
            if(task!=null)
                return Remove(context, task.ID);

            return false;
            //}
        }

        public IQueryable<DoingTasks> GetAllDoingTasks(MaindbContext context)
        {
            return GetList(context, s => s.Status == BoB.EFDbContext.Enums.DataStatus.Normal);
        }
    }
}
