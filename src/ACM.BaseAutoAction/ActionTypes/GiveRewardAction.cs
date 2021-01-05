using System;
using System.Collections.Generic;
using System.Text;

namespace ACM.BaseAutoAction.ActionTypes
{
    public class GiveRewardAction : ViewAction
    {
        /// <summary>
        /// 是否同时点赞
        /// </summary>
        public bool WithLike { get; set; }

        /// <summary>
        /// 打赏的金额
        /// </summary>
        public int Money { get; set; }
    }
}
