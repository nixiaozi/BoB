using ACM.BaseAutoAction;
using BoB.ContainManager;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Text;
using ACM.SeleniumManager;
using System.Threading;

namespace ACM.Bilibili
{
    public class BilibiliAuto : InitBlockService, IBaseAuto
    {
        private IBilibiliService _bilibiliService;

        protected override void Init()
        {
            //_bilibiliService = CurrentServiceProvider.GetService<IBilibiliService>();

        }


        public void DoBrowserRandom(RandomBrowse paramObj, CancellationToken ct)
        {
            var driver = ChromeDriverHelper.InitDriver().BrowserToUrl(BoBConfiguration.HomePage);
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
