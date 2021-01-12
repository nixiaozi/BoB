using ACM.BaseAutoAction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ACM.SinaChina
{
    public class SinaChinaAuto : IBaseAuto
    {
        public void DoBrowserRandom(RandomBrowse paramObj, CancellationToken ct)
        {
            Console.WriteLine("DoBrowserRandom");
        }

        public void DoBrowserToAttention(AttentionAction paramObj, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public void DoBrowserToBarrage(BarrageAction paramObj, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public void DoBrowserToCollect(CollectAction paramObj, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public void DoBrowserToComment(CommentAction paramObj, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public void DoBrowserToGiveLike(GiveLikeAction paramObj, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public void DoBrowserToLogin(LoginAction paramObj, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public void DoBrowserToShare(ShareAction paramObj, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public void DoBrowserToView(ViewAction paramObj, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
