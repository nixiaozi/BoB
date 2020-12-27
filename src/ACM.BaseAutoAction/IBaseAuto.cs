using System;
using System.Collections.Generic;
using System.Text;

namespace ACM.BaseAutoAction
{
    public interface IBaseAuto
    {
        public void DoBrowserRandom(RandomBrowse paramObj);

        public void DoBrowserToAttention(AttentionAction paramObj);

        public void DoBrowserToBarrage(BarrageAction paramObj);

        public void DoBrowserToCollect(CollectAction paramObj);

        public void DoBrowserToComment(CommentAction paramObj);

        public void DoBrowserToGiveLike(GiveLikeAction paramObj);

        public void DoBrowserToLogin(LoginAction paramObj);

        public void DoBrowserToShare(ShareAction paramObj);

        public void DoBrowserToView(ViewAction paramObj);


    }
}
