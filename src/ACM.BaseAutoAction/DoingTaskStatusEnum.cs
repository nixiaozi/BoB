using System;
using System.Collections.Generic;
using System.Text;

namespace ACM.BaseAutoAction
{
    public enum DoingTaskStatusEnum
    {
        /// <summary>
        /// 刚进入准备开始执行
        /// </summary>
        Prepare,
        /// <summary>
        /// 正在执行
        /// </summary>
        Doing,
        /// <summary>
        /// 线程挂起状态
        /// </summary>
        Hang,
    }
}
