using System;
using System.Collections.Generic;
using System.Text;

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

        public void DoBrowserRandom(RandomBrowse paramObj)
        {
            _baseAutoAction.DoBrowserRandom(paramObj);
        }

        public void DoBrowserToAttention(AttentionAction paramObj)
        {
            _baseAutoAction.DoBrowserToAttention(paramObj);
        }

        public void DoBrowserToBarrage(BarrageAction paramObj)
        {
            _baseAutoAction.DoBrowserToBarrage(paramObj);
        }

        public void DoBrowserToCollect(CollectAction paramObj)
        {
            _baseAutoAction.DoBrowserToCollect(paramObj);
        }

        public void DoBrowserToComment(CommentAction paramObj)
        {
            _baseAutoAction.DoBrowserToComment(paramObj);
        }

        public void DoBrowserToGiveLike(GiveLikeAction paramObj)
        {
            _baseAutoAction.DoBrowserToGiveLike(paramObj);
        }

        public void DoBrowserToLogin(LoginAction paramObj)
        {
            _baseAutoAction.DoBrowserToLogin(paramObj);
        }

        public void DoBrowserToShare(ShareAction paramObj)
        {
            _baseAutoAction.DoBrowserToShare(paramObj);
        }

        public void DoBrowserToView(ViewAction paramObj)
        {
            _baseAutoAction.DoBrowserToView(paramObj);
        }

    }
}
