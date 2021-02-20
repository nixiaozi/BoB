using System;
using System.Collections.Generic;
using System.Text;

namespace ACM.BaseAutoAction
{
    public class CommentAction : ViewAction
    {
        public string CommentText { get; set; }

        // 是否新起一个评论
        public bool IsNew { get; set; } = true;

        // 需要添加评论的评论
        public string DoCommentID { get; set; }

        //public string ToDoCommentText { get; set; }

        //public string ToDoCommentTime { get; set; }

        //public string ToDoCommentUserID { get; set; }
    }
}
