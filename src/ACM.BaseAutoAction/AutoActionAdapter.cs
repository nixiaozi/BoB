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

        public void DoBrowserRandom(RandomBrowse paramObj, CancellationToken ct)
        {
            _baseAutoAction.DoBrowserRandom(paramObj, ct);
        }

        public void DoBrowserToAttention(AttentionAction paramObj, CancellationToken ct)
        {
            _baseAutoAction.DoBrowserToAttention(paramObj, ct);
        }

        public void DoBrowserToBarrage(BarrageAction paramObj, CancellationToken ct)
        {
            _baseAutoAction.DoBrowserToBarrage(paramObj, ct);
        }

        public void DoBrowserToCollect(CollectAction paramObj, CancellationToken ct)
        {
            _baseAutoAction.DoBrowserToCollect(paramObj, ct);
        }

        public void DoBrowserToComment(CommentAction paramObj, CancellationToken ct)
        {
            _baseAutoAction.DoBrowserToComment(paramObj, ct);
        }

        public void DoBrowserToGiveLike(GiveLikeAction paramObj, CancellationToken ct)
        {
            _baseAutoAction.DoBrowserToGiveLike(paramObj, ct);
        }

        public void DoBrowserToLogin(LoginAction paramObj, CancellationToken ct)
        {
            _baseAutoAction.DoBrowserToLogin(paramObj, ct);
        }

        public void DoBrowserToShare(ShareAction paramObj, CancellationToken ct)
        {
            _baseAutoAction.DoBrowserToShare(paramObj, ct);
        }

        public void DoBrowserToView(ViewAction paramObj, CancellationToken ct)
        {
            _baseAutoAction.DoBrowserToView(paramObj,ct);
        }

    }
}
