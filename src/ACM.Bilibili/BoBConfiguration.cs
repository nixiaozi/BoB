using BoB.BoBConfiguration;
using BoB.ExtendAndHelper.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ACM.Bilibili
{
    public static class BoBConfiguration
    {
        static BoBConfiguration()
        {
            BaseBoBConfiguration.Init(Assembly.GetExecutingAssembly());
        }

        public static readonly string AppName = "哔哩哔哩视频网站";
        // [WriteAble]
        public static readonly string HomePage = "http://www.bilibili.com";

        public static readonly int AppID = 2;

        [WriteAble]
        public static readonly string xIdUserHasLogin = @"//div[@class='user-con signin']";

        [WriteAble]
        public static readonly string xIdUserUnLogin = @"//div[@class='unlogin-avatar']";

        [WriteAble]
        public static readonly string xBtnToLoginPage = @"//div[@class='unlogin-avatar']";

        [WriteAble]
        public static readonly string xLoginPageUserName = @"//input[@id='login-username']";

        [WriteAble]
        public static readonly string xLoginPagepassword = @"//input[@id='login-passwd']";

        [WriteAble]
        public static readonly string xLoginPageLoginBtn = @"//a[@class='btn btn-login']";

        [WriteAble]
        public static readonly string xCookiesCheckUserLogin = "DedeUserID";

        [WriteAble]
        public static readonly string xAllMustVideoBlocks = @"//div[@class='video-page-card'] | //div[@class='spread-module'] | //div[@class='video-card-common'] | //div[@class='video-card-reco']";

        [WriteAble]
        public static readonly string xVideoBlockVideoLink = @".//a[contains(@href,'video')]"; // 相对于元素的xpath需要以 . 开始，以定义根

        [WriteAble]
        public static readonly string xPageHasVideoPlay = @"//div[@id='bilibili-player']";

        [WriteAble]
        public static readonly string xVideoPlayArea = @".//div[contains(@class,'bilibili-player-area')]";

        [WriteAble]
        public static readonly string sVideoPlayPausedClassTag = "video-state-pause";

        [WriteAble]
        public static readonly string xVideoPlayPlayBtn = @"//div[@class='bilibili-player-dm-tip-wrap']";

        [WriteAble]
        public static readonly string xVideoReplayBtn = @"//div[@class='bilibili-player-upinfo-span restart']";

        [WriteAble]
        public static readonly string sVideoPlayEndClassTag = "video-state-ending-panel-flag";

        [WriteAble]
        public static readonly string xVideoPlayCurrentTimeSpan = @"//div[@id='bilibili-player']//span[@class='bilibili-player-video-time-now']";
        //public static readonly string xVideoPlayCurrentTimeSpan = @"//div[@id='bilibili-player']/div/div/div/div/div/div/div/div/div/div/span[@class='bilibili-player-video-time-now']";

        [WriteAble]
        public static readonly string xVideoPlayHoldTimeDisplay = @"//div[@id='bilibili-player']//span[@class='bilibili-player-video-time-total']";

    }
}
