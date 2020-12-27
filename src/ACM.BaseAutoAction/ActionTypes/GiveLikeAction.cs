using System;
using System.Collections.Generic;
using System.Text;

namespace ACM.BaseAutoAction
{
    public class GiveLikeAction
    {
        /// <summary>
        /// 是否给视频点赞
        /// </summary>
        public bool IsVideoLike { get; set; }

        /// <summary>
        /// 是否给评论点赞
        /// </summary>
        public bool IsCommentLike { get; set; }

        /// <summary>
        /// 想要点赞的评论
        /// </summary>
        public string LikedCommentText { get; set; }
        /// <summary>
        /// 想要点赞的评论时间
        /// </summary>
        public string LikedCommentTime { get; set; }
        /// <summary>
        /// 想要点赞的用户标识
        /// </summary>
        public string LikedCommentUserID { get; set; }

    }
}
