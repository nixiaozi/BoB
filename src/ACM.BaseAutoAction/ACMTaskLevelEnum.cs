using System;

namespace ACM.BaseAutoAction
{
    public enum ACMTaskLevelEnum
    {
        /// <summary>
        /// 非常高，需要立即中止低等级线程或者终止运行足够长的任务或者开新的线程来执行这个任务
        /// </summary>
        VeryHigh=10,
        /// <summary>
        /// 高，在有任务完成后选择新的任务时，默认选择此优先级的内容
        /// </summary>
        Heigh=20,
        /// <summary>
        /// 普通，正常优先级的任务
        /// </summary>
        Normal=30,
        /// <summary>
        /// 低优先级，只有其他等级的任务都执行完成才会执行这个任务。
        /// </summary>
        Low=40,
    }
}
