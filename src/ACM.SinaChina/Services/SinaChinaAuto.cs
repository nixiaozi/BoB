using ACM.BaseAutoAction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ACM.SinaChina
{
    public class SinaChinaAuto : IBaseAuto
    {
        public void DoBrowserRandom(Guid userID, RandomBrowse paramObj, CancellationToken ct)
        {
            Console.WriteLine("DoBrowserRandom");
        }

        public void DoBrowserToAttention(Guid userID, AttentionAction paramObj, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public void DoBrowserToBarrage(Guid userID, BarrageAction paramObj, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public void DoBrowserToCollect(Guid userID, CollectAction paramObj, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public void DoBrowserToComment(Guid userID, CommentAction paramObj, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public void DoBrowserToGiveLike(Guid userID, GiveLikeAction paramObj, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public void DoBrowserToLogin(Guid userID, LoginAction paramObj, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public void DoBrowserToShare(Guid userID, ShareAction paramObj, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public void DoBrowserToView(Guid userID, ViewAction paramObj, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
