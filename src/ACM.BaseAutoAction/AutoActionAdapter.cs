using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ACM.BaseAutoAction
{
    public class AutoActionAdapter
    {
        private IBaseAuto _baseAutoAction;
        private string  _commandText;

        public AutoActionAdapter(IBaseAuto baseAutoAction,string commandText)
        {
            _baseAutoAction = baseAutoAction;
            _commandText = commandText;
        }


        public string CommandText
        {
            get { return _commandText; }
        }

        public void DoBrowserRandom(Guid userID, RandomBrowse paramObj, CancellationToken ct)
        {
            _baseAutoAction.DoBrowserRandom(userID, paramObj, ct);
        }

        public void DoBrowserToAttention(Guid userID, AttentionAction paramObj, CancellationToken ct)
        {
            _baseAutoAction.DoBrowserToAttention(userID, paramObj, ct);
        }

        public void DoBrowserToBarrage(Guid userID, BarrageAction paramObj, CancellationToken ct)
        {
            _baseAutoAction.DoBrowserToBarrage(userID, paramObj, ct);
        }

        public void DoBrowserToCollect(Guid userID, CollectAction paramObj, CancellationToken ct)
        {
            _baseAutoAction.DoBrowserToCollect(userID, paramObj, ct);
        }

        public void DoBrowserToComment(Guid userID, CommentAction paramObj, CancellationToken ct)
        {
            _baseAutoAction.DoBrowserToComment(userID, paramObj, ct);
        }

        public void DoBrowserToGiveLike(Guid userID, GiveLikeAction paramObj, CancellationToken ct)
        {
            _baseAutoAction.DoBrowserToGiveLike(userID, paramObj, ct);
        }

        public void DoBrowserToLogin(Guid userID, LoginAction paramObj, CancellationToken ct)
        {
            _baseAutoAction.DoBrowserToLogin(userID, paramObj, ct);
        }

        public void DoBrowserToShare(Guid userID, ShareAction paramObj, CancellationToken ct)
        {
            _baseAutoAction.DoBrowserToShare(userID, paramObj, ct);
        }

        public void DoBrowserToView(Guid userID, ViewAction paramObj, CancellationToken ct)
        {
            _baseAutoAction.DoBrowserToView(userID, paramObj,ct);
        }

    }
}
