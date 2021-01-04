using System;
using System.Collections.Generic;
using System.Text;

namespace ACM.BaseAutoAction
{
    public interface IBaseAuto
    {
        /// <summary>
        /// 随机浏览
        /// </summary>
        /// <param name="paramObj"></param>
        public void DoBrowserRandom(RandomBrowse paramObj);

       /// <summary>
       /// 加关注
       /// </summary>
       /// <param name="paramObj"></param>
        public void DoBrowserToAttention(AttentionAction paramObj);

        /// <summary>
        /// 发弹幕
        /// </summary>
        /// <param name="paramObj"></param>
        public void DoBrowserToBarrage(BarrageAction paramObj);

        /// <summary>
        /// 添加收藏
        /// </summary>
        /// <param name="paramObj"></param>
        public void DoBrowserToCollect(CollectAction paramObj);

        /// <summary>
        /// 进行评论
        /// </summary>
        /// <param name="paramObj"></param>
        public void DoBrowserToComment(CommentAction paramObj);

        /// <summary>
        /// 进行点赞操作
        /// </summary>
        /// <param name="paramObj"></param>
        public void DoBrowserToGiveLike(GiveLikeAction paramObj);

        /// <summary>
        /// 进行登录操作，为了每日积分
        /// </summary>
        /// <param name="paramObj"></param>
        public void DoBrowserToLogin(LoginAction paramObj);

        /// <summary>
        /// 进行分享操作
        /// </summary>
        /// <param name="paramObj"></param>
        public void DoBrowserToShare(ShareAction paramObj);

        /// <summary>
        /// 为了观看特定页的操作
        /// </summary>
        /// <param name="paramObj"></param>
        public void DoBrowserToView(ViewAction paramObj);


    }
}
