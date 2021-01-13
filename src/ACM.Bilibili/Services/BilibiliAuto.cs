using ACM.BaseAutoAction;
using BoB.ContainManager;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Text;
using ACM.SeleniumManager;
using System.Threading;
using Autofac;
using OpenQA.Selenium.Chrome;
using ACM.AppAccountListEntities;
using BoB.EmailManager;
using ACM.UserEntities;
using BoB.ExtendAndHelper.Utilties;

namespace ACM.Bilibili
{
    public class BilibiliAuto : InitBlockService, IBaseAuto
    {
        
        private IAppAccountListBlock _appAccountListBlock;
        private IEmailManagerService _emailManagerService;
        private IUserBlock _userBlock;

        protected override void Init()
        {
            _appAccountListBlock= BoBContainer.ServiceContainer.Resolve<IAppAccountListBlock>();
            _emailManagerService = CurrentServiceContainer.Resolve<IEmailManagerService>();
            _userBlock = CurrentServiceContainer.Resolve<IUserBlock>();

        }

        private ChromeDriver InsureUserHasLogin(Guid userID, CancellationToken ct, string StartUrl=null)
        {
            if (userID == Guid.Empty)
            {
                throw new Exception("用户编号不能为空！");
            }
            ChromeDriver driver;

            driver = ChromeDriverHelper.InitDriver(@"D:\publish\chromeProfiles\" + userID.ToString().ToUpper())
                    .BrowserToUrl(String.IsNullOrWhiteSpace(StartUrl) ? BoBConfiguration.HomePage : StartUrl);

            bool IsUnLogined;
            driver.CheckElementUseable(BoBConfiguration.xIdUserUnLogin, out IsUnLogined, true, null, ct);
            bool IsLogined;
            driver.CheckElementUseable(BoBConfiguration.xIdUserHasLogin, out IsLogined, true, null, ct);


            if (IsLogined)
            {
                return driver;
            }
            else
            {
                var account = _appAccountListBlock.GetAccountByUser(userID, BoBConfiguration.AppID);
                var user = _userBlock.GetUserById(userID);
                if (!IsUnLogined) // 不能判断它未登录
                {
                    driver.BrowserToUrl(BoBConfiguration.HomePage, null, ct); // 回到首页
                }
                // 再次判断特征元素
                driver.CheckElementUseable(BoBConfiguration.xIdUserUnLogin, out IsUnLogined, false, (seconds,url)=> 
                {
                    if (seconds == 100)
                    {
                        _emailManagerService.ACMEmailAutoWarn("应用：" + BoBConfiguration.AppName + 
                            "; 在用户：" + account.NickName + "，确认登录状况时出现异常。应该没有登录，但是找不到登录按钮");
                    }
                }, ct);

                // 下面直接访问登录页添加输入用户名和密码进行登录
                driver.LeftClickElement(BoBConfiguration.xBtnToLoginPage, null, ct);

                // 输入用户名
                driver.InputContext(BoBConfiguration.xLoginPageUserName, user.Phone.Trim(), (seconds, url) =>
                {
                    if (seconds == 100)
                    {
                        _emailManagerService.ACMEmailAutoWarn("应用：" + BoBConfiguration.AppName +
                            "; 在用户：" + account.NickName + "，登录时出现异常。无法输入用户名");
                    }
                }, ct);

                // 输入密码
                driver.InputContext(BoBConfiguration.xLoginPagepassword,SecurityHelper.DecryptFromBase64(account.Password,account.Salt), 
                    (seconds, url) =>
                {
                    if (seconds == 100)
                    {
                        _emailManagerService.ACMEmailAutoWarn("应用：" + BoBConfiguration.AppName +
                            "; 在用户：" + account.NickName + "，登录时出现异常。无法输入密码");
                    }
                }, ct);

                // 点击按钮进行登录
                driver.LeftClickElement(BoBConfiguration.xLoginPageLoginBtn, null, ct);

                // 通过查看cookies 确认是否已经登录
                bool HasUserLoginByCookies;
                driver.CheckExistsCookieName(BoBConfiguration.xCookiesCheckUserLogin, out HasUserLoginByCookies, false, (seconds, url) =>
                {
                    if (seconds == 100)
                    {
                        _emailManagerService.ACMEmailAutoWarn("应用：" + BoBConfiguration.AppName +
                            "; 在用户：" + account.NickName + "，登录时出现异常。无法成功登录，需要手动处理");
                    }
                }, ct);


            }


            return driver;
        }

        

        public void DoBrowserRandom(Guid userID,RandomBrowse paramObj, CancellationToken ct)
        {
            ChromeDriver driver=new ChromeDriver();
            try
            {

                driver = InsureUserHasLogin(userID, ct, paramObj.StartUrl);
                // 可以定义几个不同的行为 比如 查看动态 查看热门榜单 首页(栏目随机选取)随机选取  详细页你可能系统 
                driver = driver.ToSomeTargetPage(ct, new ViewAction()); // 办正事

                // 下面进入随机浏览模式
                driver = driver.ToSomeTargetPage(ct, null); // 下面进入随机主体页模式
                driver = driver.JustRandomBowserVideoUrl(ct); // 随机点击进入视频详细
                driver = driver.ViewVideoToEnd(ct, (startTime, endTime) =>
                {
                    driver.PrintBrowserLog("此时的播放区间为 开始时间：" + startTime.ToString("HH:mm:ss") + ";结束时间为：" + endTime.ToString("HH:mm:ss"));
                }); // 播放当前视频直到播放完成
            }
            catch (AggregateException e)
            {
                driver.Close();
                driver.Quit();
                throw e; // 抛出自动取消异常
            }
            catch(Exception ex)
            {
                driver.Close();
                driver.Quit();
                throw ex;
            }

        }

        public void DoBrowserToAttention(Guid userID, AttentionAction paramObj, CancellationToken ct)
        {
            var driver = InsureUserHasLogin(userID, ct, paramObj.StartUrl);

        }

        public void DoBrowserToBarrage(Guid userID, BarrageAction paramObj, CancellationToken ct)
        {
            var driver = InsureUserHasLogin(userID, ct, paramObj.StartUrl);

        }

        public void DoBrowserToCollect(Guid userID, CollectAction paramObj, CancellationToken ct)
        {
            var driver = InsureUserHasLogin(userID, ct, paramObj.StartUrl);

        }

        public void DoBrowserToComment(Guid userID, CommentAction paramObj, CancellationToken ct)
        {
            var driver = InsureUserHasLogin(userID, ct, paramObj.StartUrl);

        }

        public void DoBrowserToGiveLike(Guid userID, GiveLikeAction paramObj, CancellationToken ct)
        {
            var driver = InsureUserHasLogin(userID, ct, paramObj.StartUrl);

        }

        public void DoBrowserToLogin(Guid userID, LoginAction paramObj, CancellationToken ct)
        {
            var driver = InsureUserHasLogin(userID, ct, paramObj.StartUrl);

        }

        public void DoBrowserToShare(Guid userID, ShareAction paramObj, CancellationToken ct)
        {
            var driver = InsureUserHasLogin(userID, ct, paramObj.StartUrl);

        }

        public void DoBrowserToView(Guid userID, ViewAction paramObj, CancellationToken ct)
        {
            var driver = InsureUserHasLogin(userID, ct, paramObj.StartUrl);

        }
    }
}
