using ACM.BaseAutoAction;
using BoB.ContainManager;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Text;
using ACM.SeleniumManager;

namespace ACM.Bilibili
{
    public class BilibiliAuto : InitBlockService, IBaseAuto
    {
        private IBilibiliService _bilibiliService;

        protected override void Init()
        {
            //_bilibiliService = CurrentServiceProvider.GetService<IBilibiliService>();

        }


        public void DoBrowserRandom(RandomBrowse paramObj)
        {
            var driver = ChromeDriverHelper.InitDriver().BrowserToUrl(BoBConfiguration.HomePage);
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
