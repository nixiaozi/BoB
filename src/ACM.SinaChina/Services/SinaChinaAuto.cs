using ACM.BaseAutoAction;
using System;
using System.Collections.Generic;
using System.Text;

namespace ACM.SinaChina
{
    public class SinaChinaAuto : IBaseAuto
    {
        public void DoBrowserRandom(RandomBrowse paramObj)
        {
            Console.WriteLine("DoBrowserRandom");
        }

        public void DoBrowserToAttention(AttentionAction paramObj)
        {
            throw new NotImplementedException();
        }

        public void DoBrowserToBarrage(BarrageAction paramObj)
        {
            throw new NotImplementedException();
        }

        public void DoBrowserToCollect(CollectAction paramObj)
        {
            throw new NotImplementedException();
        }

        public void DoBrowserToComment(CommentAction paramObj)
        {
            throw new NotImplementedException();
        }

        public void DoBrowserToGiveLike(GiveLikeAction paramObj)
        {
            throw new NotImplementedException();
        }

        public void DoBrowserToLogin(LoginAction paramObj)
        {
            throw new NotImplementedException();
        }

        public void DoBrowserToShare(ShareAction paramObj)
        {
            throw new NotImplementedException();
        }

        public void DoBrowserToView(ViewAction paramObj)
        {
            throw new NotImplementedException();
        }
    }
}
