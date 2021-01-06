using ACM.MainDatabase;
using ACM.TaskManager.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ACM.TaskManager
{
    public interface ITaskManagerService
    {
        public List<TaskDetailOutput> GetDoingTaskDetail(MaindbContext context = null);


        public List<TaskDetailOutput> GetUndoneTaskDetail(MaindbContext context = null);
    }
}
