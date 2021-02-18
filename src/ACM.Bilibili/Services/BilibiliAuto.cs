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

        private ChromeDriver ChromeInit(Guid userID)
        {
            var driver = ChromeDriverHelper.InitDriver(ACM.SeleniumManager.BoBConfiguration.ChromeProfilePath + userID.ToString().ToUpper());
            return driver;
        }

        private ChromeDriver ChromeReplayVideo(ChromeDriver driver, CancellationToken ct)
        {
            var time = BoBConfiguration.ReplayTimes;
            while (time >= 0)
            {
                driver.LeftClickElement(BoBConfiguration.xVideoReplayBtn, null, ct);
                driver.ViewVideoToEnd(ct, null, null);  // 开始查看视频

                time--;
            }

            return driver;
        }

        private ChromeDriver InsureUserHasLogin(ChromeDriver driver,Guid userID, CancellationToken ct, string StartUrl=null)
        {
            if (userID == Guid.Empty)
            {
                throw new Exception("用户编号不能为空！");
            }



            driver = driver.BrowserToUrl(String.IsNullOrWhiteSpace(StartUrl) ? BoBConfiguration.HomePage : StartUrl);

            bool IsUnLogined;
            driver = driver.CheckElementUseable(BoBConfiguration.xIdUserUnLogin, out IsUnLogined, true, null, ct);
            bool IsLogined;
            driver = driver.CheckElementUseable(BoBConfiguration.xIdUserHasLogin, out IsLogined, true, null, ct);


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
                    driver = driver.BrowserToUrl(BoBConfiguration.HomePage, null, ct); // 回到首页
                }
                // 再次判断特征元素
                driver = driver.CheckElementUseable(BoBConfiguration.xIdUserUnLogin, out IsUnLogined, false, (seconds,url)=> 
                {
                    if (seconds == 100)
                    {
                        _emailManagerService.ACMEmailAutoWarn("应用：" + BoBConfiguration.AppName + 
                            "; 在用户：" + account.NickName + "，确认登录状况时出现异常。应该没有登录，但是找不到登录按钮");
                    }
                }, ct);

                // 下面直接访问登录页添加输入用户名和密码进行登录
                driver = driver.LeftClickElement(BoBConfiguration.xBtnToLoginPage, null, ct);

                // Selenium 不会自动切换页面，需要手动
                driver = driver.SwitchToNewWindow(ct);


                // 输入用户名
                driver = driver.InputContext(BoBConfiguration.xLoginPageUserName, user.Phone.Trim(), (seconds, url) =>
                {
                    if (seconds == 100)
                    {
                        _emailManagerService.ACMEmailAutoWarn("应用：" + BoBConfiguration.AppName +
                            "; 在用户：" + account.NickName + "，登录时出现异常。无法输入用户名");
                    }
                }, ct);

                // 输入密码
                driver = driver.InputContext(BoBConfiguration.xLoginPagepassword,SecurityHelper.DecryptFromBase64(account.Password,account.Salt), 
                    (seconds, url) =>
                {
                    if (seconds == 100)
                    {
                        _emailManagerService.ACMEmailAutoWarn("应用：" + BoBConfiguration.AppName +
                            "; 在用户：" + account.NickName + "，登录时出现异常。无法输入密码");
                    }
                }, ct);

                // 点击按钮进行登录
                driver = driver.LeftClickElement(BoBConfiguration.xLoginPageLoginBtn, null, ct);

                // 通过查看cookies 确认是否已经登录
                bool HasUserLoginByCookies;
                driver = driver.CheckExistsCookieName(BoBConfiguration.xCookiesCheckUserLogin, out HasUserLoginByCookies, false, (seconds, url) =>
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

        private ChromeDriver RandomBrowserAction(ChromeDriver driver, CancellationToken ct)
        {
            bool DoAgain = true;
            Random random = new Random();
            while (DoAgain)
            {

                // 下面进入随机浏览模式
                driver.ToSomeTargetPage(ct, null); // 下面进入随机主体页模式
                driver.JustRandomBowserVideoUrl(ct); // 随机点击进入视频详细
                driver.ViewVideoToEnd(ct, (startTime, endTime, totalTime) =>
                {
                    driver.PrintBrowserLog("此时的播放区间为 开始时间：" + startTime.ToString("HH:mm:ss") + ";结束时间为：" + endTime.ToString("HH:mm:ss"));
                }); // 播放当前视频直到播放完成

                if (random.Next(0, 100) < 27)
                {
                    DoAgain = false;
                }

            }

            return driver;
        }

        public void DoBrowserRandom(Guid userID,RandomBrowse paramObj, CancellationToken ct)
        {
            ChromeDriver driver = ChromeInit(userID);
            try
            {
                driver = InsureUserHasLogin(driver, userID, ct, paramObj.StartUrl);
                // 可以定义几个不同的行为 比如 查看动态 查看热门榜单 首页(栏目随机选取)随机选取  详细页你可能系统 
                driver = driver.ToSomeTargetPage(ct, new ViewAction()); // 办正事

                // 下面进入随机浏览模式
                driver = driver.ToSomeTargetPage(ct, null); // 下面进入随机主体页模式
                driver = driver.JustRandomBowserVideoUrl(ct); // 随机点击进入视频详细
                driver = driver.ViewVideoToEnd(ct, (startTime, endTime, totalTime) =>
                {
                    driver.PrintBrowserLog("此时的播放区间为 开始时间：" + startTime.ToString("HH:mm:ss") + ";结束时间为：" + endTime.ToString("HH:mm:ss"));
                }); // 播放当前视频直到播放完成

                driver.ExitDriver();
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
            ChromeDriver driver = ChromeInit(userID);
            try
            {
               InsureUserHasLogin(driver, userID, ct, paramObj.StartUrl);
                // 可以定义几个不同的行为 比如 查看动态 查看热门榜单 首页(栏目随机选取)随机选取  详细页你可能系统 
                driver.ToSomeTargetPage(ct, paramObj); // 办正事 进入页面
                driver.ViewVideoToEnd(ct, (startTime, endTime,totalTime) => // 看视频办正事
                {
                    // 通过一个比较播放的比例是大于随机数来确认是否进行关注
                    bool HasAttended;
                    driver.CheckElementIsExists(BoBConfiguration.xUserFollowBtn, out HasAttended, true, null, ct);

                    if (!HasAttended)
                    {
                        Random random = new Random();
                        var playedPercent = endTime.Ticks / totalTime.Ticks;
                        var randomPercent = random.NextDouble();
                        if (randomPercent >= playedPercent)
                        {
                            driver.LeftClickElement(BoBConfiguration.xUserFollowBtn, null, ct);
                        }

                    }

                });


                // 下面进入随机浏览模式
                RandomBrowserAction(driver, ct);


                driver.ExitDriver();

            }
            catch (AggregateException e)
            {
                driver.Close();
                driver.Quit();
                throw e; // 抛出自动取消异常
            }
            catch (Exception ex)
            {
                driver.Close();
                driver.Quit();
                throw ex;
            }
        }

        public void DoBrowserToBarrage(Guid userID, BarrageAction paramObj, CancellationToken ct)
        {
            ChromeDriver driver = ChromeInit(userID);
            try
            {
                InsureUserHasLogin(driver, userID, ct, paramObj.StartUrl);
                // 可以定义几个不同的行为 比如 查看动态 查看热门榜单 首页(栏目随机选取)随机选取  详细页你可能系统 
                driver.ToSomeTargetPage(ct, paramObj); // 办正事 进入页面

                var HasSend = false;
                driver.ViewVideoToEnd(ct, (startTime, endTime, totalTime) => // 看视频办正事
                {
                    if (!HasSend && paramObj.BarrageTime >=startTime && paramObj.BarrageTime <= endTime)
                    {
                        // 输入内容
                        driver.InputContext(BoBConfiguration.xSendDanmuInput, paramObj.BarrageText, null, ct);

                        // 发送弹幕
                        driver.LeftClickElement(BoBConfiguration.xSendDanmuBtn, null, ct);

                        HasSend = true;
                    }

                });


                // 下面进入随机浏览模式
                RandomBrowserAction(driver, ct);


                driver.ExitDriver();

            }
            catch (AggregateException e)
            {
                driver.Close();
                driver.Quit();
                throw e; // 抛出自动取消异常
            }
            catch (Exception ex)
            {
                driver.Close();
                driver.Quit();
                throw ex;
            }
        }

        public void DoBrowserToCollect(Guid userID, CollectAction paramObj, CancellationToken ct)
        {
            ChromeDriver driver = ChromeInit(userID);
            try
            {
                InsureUserHasLogin(driver, userID, ct, paramObj.StartUrl);
                // 可以定义几个不同的行为 比如 查看动态 查看热门榜单 首页(栏目随机选取)随机选取  详细页你可能系统 
                driver.ToSomeTargetPage(ct, paramObj); // 办正事 进入页面
                driver.ViewVideoToEnd(ct, (startTime, endTime, totalTime) => // 看视频办正事
                {
                    // 办正事了专用


                });


                // 下面进入随机浏览模式
                RandomBrowserAction(driver, ct);


                driver.ExitDriver();

            }
            catch (AggregateException e)
            {
                driver.Close();
                driver.Quit();
                throw e; // 抛出自动取消异常
            }
            catch (Exception ex)
            {
                driver.Close();
                driver.Quit();
                throw ex;
            }
        }

        public void DoBrowserToComment(Guid userID, CommentAction paramObj, CancellationToken ct)
        {
            ChromeDriver driver = ChromeInit(userID);
            try
            {
                InsureUserHasLogin(driver, userID, ct, paramObj.StartUrl);
                // 可以定义几个不同的行为 比如 查看动态 查看热门榜单 首页(栏目随机选取)随机选取  详细页你可能系统 
                driver.ToSomeTargetPage(ct, paramObj); // 办正事 进入页面
                driver.ViewVideoToEnd(ct, (startTime, endTime, totalTime) => // 看视频办正事
                {
                    // 办正事了专用



                });


                // 下面进入随机浏览模式
                RandomBrowserAction(driver, ct);


                driver.ExitDriver();

            }
            catch (AggregateException e)
            {
                driver.Close();
                driver.Quit();
                throw e; // 抛出自动取消异常
            }
            catch (Exception ex)
            {
                driver.Close();
                driver.Quit();
                throw ex;
            }
        }

        public void DoBrowserToGiveLike(Guid userID, GiveLikeAction paramObj, CancellationToken ct)
        {
            ChromeDriver driver = ChromeInit(userID);
            try
            {
                InsureUserHasLogin(driver, userID, ct, paramObj.StartUrl);
                // 可以定义几个不同的行为 比如 查看动态 查看热门榜单 首页(栏目随机选取)随机选取  详细页你可能系统 
                driver.ToSomeTargetPage(ct, paramObj); // 办正事 进入页面
                driver.ViewVideoToEnd(ct, (startTime, endTime, totalTime) => // 看视频办正事
                {
                    // 办正事了专用



                });


                // 下面进入随机浏览模式
                RandomBrowserAction(driver, ct);


                driver.ExitDriver();

            }
            catch (AggregateException e)
            {
                driver.Close();
                driver.Quit();
                throw e; // 抛出自动取消异常
            }
            catch (Exception ex)
            {
                driver.Close();
                driver.Quit();
                throw ex;
            }
        }

        public void DoBrowserToLogin(Guid userID, LoginAction paramObj, CancellationToken ct)
        {
            ChromeDriver driver = ChromeInit(userID);
            try
            {
                InsureUserHasLogin(driver, userID, ct, paramObj.StartUrl);

                driver.PrintBrowserLog("登录操作已完成，你可以随时的安全退出！");

                // 登录之后可以自行操作。

                // 一下为测试代码
                driver.PrintBrowserLog(driver.GetVideoAVstring(ct));

            }
            catch (AggregateException e)
            {
                driver.Close();
                driver.Quit();
                throw e; // 抛出自动取消异常
            }
            catch (Exception ex)
            {
                driver.Close();
                driver.Quit();
                throw ex;
            }

        }

        public void DoBrowserToShare(Guid userID, ShareAction paramObj, CancellationToken ct)
        {
            ChromeDriver driver = ChromeInit(userID);
            try
            {
                InsureUserHasLogin(driver, userID, ct, paramObj.StartUrl);
                // 这里做正事
                driver.ToSomeTargetPage(ct, paramObj); // 转到查看页
                driver.ViewVideoToEnd(ct, (startTime,endTime,totalTime)=> { 
                    

                }, null);  


                RandomBrowserAction(driver, ct);
                driver.ExitDriver();

            }
            catch (AggregateException e)
            {
                driver.Close();
                driver.Quit();
                throw e; // 抛出自动取消异常
            }
            catch (Exception ex)
            {
                driver.Close();
                driver.Quit();
                throw ex;
            }

        }

        public void DoBrowserToView(Guid userID, ViewAction paramObj, CancellationToken ct)
        {
            ChromeDriver driver = ChromeInit(userID);
            try
            {
                InsureUserHasLogin(driver, userID, ct, paramObj.StartUrl);
                // 这里做正事
                driver.ToSomeTargetPage(ct, paramObj); // 转到查看页
                driver.ViewVideoToEnd(ct, null, null);  // 开始查看视频

                // 可以调整重播的次数
                ChromeReplayVideo(driver, ct);


                RandomBrowserAction(driver, ct);

                driver.ExitDriver(); // 顺利执行完成之后需要退出

            }
            catch (AggregateException e)
            {
                driver.Close();
                driver.Quit();
                throw e; // 抛出系统自动取消异常
            }
            catch (Exception ex)
            {
                driver.Close();
                driver.Quit();
                throw ex;
            }
        }



    }
}
