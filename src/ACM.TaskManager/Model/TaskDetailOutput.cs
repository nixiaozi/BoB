using ACM.BaseAutoAction;
using BoB.ExtendAndHelper.Extends;
using System;
using System.Collections.Generic;
using System.Text;

namespace ACM.TaskManager.Model
{
    public class TaskDetailOutput
    {
        public Guid TaskID { get; set; }

        public ACMTaskTypeEnum TaskType { get; set; }


        public ACMTaskLevelEnum TaskLevel { get; set; }

        public string TaskParams { get; set; }

        public TaskExecuteStatusEnum TaskExecuteStatus { get; set; }

        public DoingTaskStatusEnum? DoingTaskStatus { get; set; } // 可以为空

        public DateTime CreateDate { get; set; }

        public int AppID { get; set; }

        public Guid UserID { get; set; }

        public string TheTaskStatus
        {
            get
            {
                if (this.DoingTaskStatus.HasValue)
                {
                    return this.TaskExecuteStatus.DisplayName() + "(" + this.DoingTaskStatus.Value + ")";
                }
                else
                {
                    return this.TaskExecuteStatus.DisplayName() + "(未执行)";
                }

            }
        }

    }
}
