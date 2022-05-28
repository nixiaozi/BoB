using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ACM.BaseAutoAction
{
     public enum ACMTaskTypeEnum
    {
        [Display(Name ="仅随机浏览")]
        RandomBrowse,
        [Display(Name = "目标浏览任务")]
        View,
        [Display(Name = "关注任务")]
        Attention,
        [Display(Name = "发送弹幕任务")]
        Barrage,
        [Display(Name = "收藏任务")]
        Collect,
        [Display(Name = "评论任务")]
        Comment,
        [Display(Name = "点赞任务")]
        GiveLike,
        [Display(Name = "登录上线任务")]
        Login,
        [Display(Name = "分享任务")]
        Share,
        [Display(Name = "打赏任务")]
        GiveReward

    }
}
