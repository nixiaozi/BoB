using BoB.BoBConfiguration;
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


        public static readonly string xIdUserHasLogin = @"//div[@class='bili-avatar']";

        public static readonly string xIdUserUnLogin = @"//div[@class='user-con logout']";

        public static readonly string xBtnToLoginPage = @"//img[@class='logout-face']";

        public static readonly string xLoginPageUserName = @"//input[@id='login-username']";

        public static readonly string xLoginPagepassword = @"//input[@id='login-passwd']";

        public static readonly string xLoginPageLoginBtn = @"//a[@class='btn btn-login']";

        public static readonly string xCookiesCheckUserLogin = "DedeUserID";

        public static readonly string xAllMustVideoBlocks = @"//div[@class='video-page-card'] | //div[@class='spread-module'] | //div[@class='video-card-common']";

        public static readonly string xVideoBlockVideoLink = @"//a[contains(@href,'video')]";

        public static readonly string xPageHasVideoPlay = @"//div[@id='bilibili-player']";

        public static readonly string xVideoPlayArea = @"//div[contains(@class,'bilibili-player-area')]";

        public static readonly string sVideoPlayPausedClassTag = "video-state-pause";

        public static readonly string xVideoPlayPlayBtn = @"//div[@class='bilibili-player-video-state']";

        public static readonly string sVideoPlayEndClassTag = "video-state-ending-panel-flag";

        public static readonly string xVideoPlayCurrentTimeSpan = @"//div[@id='bilibili-player']//span[@class='bilibili-player-video-time-now']";

    }
}
