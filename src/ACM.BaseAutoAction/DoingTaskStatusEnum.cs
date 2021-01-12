using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ACM.BaseAutoAction
{
    public enum DoingTaskStatusEnum
    {
        /// <summary>
        /// 刚进入准备开始执行
        /// </summary>
        [Display(Name = "准备执行")]
        Prepare,
        /// <summary>
        /// 正在执行
        /// </summary>
        [Display(Name = "正在执行")]
        Doing,

    }
}
