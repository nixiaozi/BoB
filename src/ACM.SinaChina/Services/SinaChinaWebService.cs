using ACM.AppAccountListEntities;
using ACM.UserEntities;
using BoB.ContainManager;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using BoB.ExtendAndHelper.Utilties;
using System.Threading;
using BoB.EmailManager;
using ACM.SeleniumManager;
using Autofac;

namespace ACM.SinaChina
{
    public class SinaChinaWebService : InitBlockService, ISinaChinaWebService
    {
        private IUserBlock _userBlock;
        private IAppAccountListBlock _appAccountListBlock;
        private IEmailManagerService _emailManagerService;
        protected override void Init()
        {
            _userBlock = CurrentServiceContainer.Resolve<IUserBlock>();
            _appAccountListBlock = CurrentServiceContainer.Resolve<IAppAccountListBlock>();
            _emailManagerService = CurrentServiceContainer.Resolve<IEmailManagerService>();
        }

        public bool ToLogin(AppAccountList account)
        {
            var user = _userBlock.GetUserById(account.UserID);
            Debug.Print("开始自动登录");

            #region InitDriver 可以自定义Cookies保存目录
            //ChromeOptions chromeOptions = new ChromeOptions();
            //chromeOptions.AddArgument("ignore-certificate-errors");
            //chromeOptions.AddArgument("--ignore-ssl-errors");
            //chromeOptions.AcceptInsecureCertificates = true; // 准许不安全的证书

            ////chromeOptions.AddAdditionalCapability("ACCEPT_SSL_CERTS", true);




            //var driver = new ChromeDriver(chromeOptions);
            var driver = ChromeDriverHelper.InitDriver(ACM.SeleniumManager.BoBConfiguration.ChromeProfilePath + user.ID.ToString().ToUpper());

            #endregion

            #region CustomLocation
            //下面是添加自定义的地理位置信息
            //Dictionary<string, object> coordinates = new Dictionary<string, object>
            //{
            //    {"latitude", 50.2334 },
            //    {"longitude", 0.2334 },
            //    {"accuracy", 1 }
            //};

            //driver.ExecuteChromeCommand("Emulation.setGeolocationOverride", coordinates);
            driver = driver.CustomLocation(50.2334d, 0.2334d);

            #endregion


            #region ResizeWindow
            //driver.Manage().Window.Size = new Size(1024, 768);

            driver = driver.ResizeWindow(1024, 768);
            #endregion

            #region BrowserToUrl
            //driver.Navigate().GoToUrl(BoBConfiguration.HomePage);
            driver = driver.BrowserToUrl(BoBConfiguration.HomePage);
            #endregion

            #region CurrentUrlSetCookies  没有效用的方法
            // 需要获取用户并且进行登录
            //var cookieCollection = account.Cookie == null ? null : JsonConvert.DeserializeObject<List<Cookie>>(account.Cookie);
            //if (cookieCollection != null)
            //    cookieCollection.ForEach(s => driver.Manage().Cookies.AddCookie(s));
            // driver = driver.CurrentDomainSetCookies(account.Cookie);
            #endregion

            //// test lambel status
            //driver.ExecuteScript("console.info('{0}')", "Test并正常返回数据直接");
            //driver.PrintBrowserLog("Test并正常返回数据。");
            //bool testresult;
            //var test = driver.CheckElementExist(@"//nav/a[contains(@class,'hd_s_set')]", out testresult);

            #region LeftClickElemen 设置按钮
            //IWebElement element = driver.FindElementByXPath(@"//nav/a[contains(@class,'hd_s_set')]"); // 找到设置按钮
            //Actions actionProvider = new Actions(driver);
            //actionProvider.Click(element).Build().Perform();   // 点击设置按钮

            driver = driver.LeftClickElement(@"//nav/a[contains(@class,'hd_s_set')]");
            #endregion

            #region CheckElementUseable && Click
            // 条件判断 只有未登录情况下才进行登录操作
            //try
            //{
            //    var loginBtn = new WebDriverWait(driver, TimeSpan.FromSeconds(3))
            //            .Until(drv => drv.FindElement(By.Id("j_login_btn")));
            //    Actions loginAction = new Actions(driver); // 必须要使用新建的Actions 否则会出错

            //    loginAction.Click(loginBtn).Build().Perform();
            //}
            //catch (Exception ex)
            //{
            //    driver.Quit(); // 用户已登录
            //    return true;
            //}
            bool IsMustLogin;
            driver = driver.CheckElementUseable(@"//a[@id='j_login_btn']", out IsMustLogin,true);
            if (IsMustLogin)
            {
                // 到达登录页
                driver = driver.LeftClickElement(@"//a[@id='j_login_btn']");
            }


            #endregion

            #region CheckElementUseable 查找登录按钮确定准许登录
            //// 获取进行登录操作的按钮
            //var realLoginBtn = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
            //    //.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath(@"/html/body//a[@id='loginAction']"))); // ExpectedConditions is Obsolete
            //    .Until<IWebElement>(drv =>
            //    {
            //        IWebElement templateEle = drv.FindElement(By.XPath(@"/html/body//a[@id='loginAction']"));
            //        return templateEle.Displayed && templateEle.Enabled ? templateEle : null;
            //    });
            bool IsCanLogin;
            driver = driver.CheckElementUseable(@"/html/body//a[@id='loginAction']", out IsCanLogin,true); // 允许自动取消
            #endregion

            #region InputContext 输入用户名和密码
            //// 填写用户名和密码
            //var userNameText = driver.FindElementByXPath(@"/html/body//input[@id='loginName']");
            //var passwordText = driver.FindElementByXPath(@"/html/body//input[@id='loginPassword']");

            //var userNameStr = user.Phone.Trim();
            //Random random = new Random();
            ////userNameText.SendKeys(userNameStr); //  important：正式环境下需要从数据库中获取数据
            //// 模拟用户输入，不要复制
            //for (var i = 0; i < userNameStr.Length; i++)
            //{

            //    Thread.Sleep(random.Next(360, 877));
            //    userNameText.SendKeys(userNameStr[i].ToString());
            //}

            ////passwordText.SendKeys(SecurityHelper.DecryptFromBase64(account.Password.Trim(), account.Salt.Trim()));
            //var passwordStr = SecurityHelper.DecryptFromBase64(account.Password.Trim(), account.Salt.Trim());
            //for (var i = 0; i < passwordStr.Length; i++)
            //{
            //    Thread.Sleep(random.Next(360, 877));
            //    passwordText.SendKeys(passwordStr[i].ToString());
            //}
            driver = driver.InputContext(@"/html/body//input[@id='loginName']", user.Phone);
            driver = driver.InputContext(@"/html/body//input[@id='loginPassword']",
                SecurityHelper.DecryptFromBase64(account.Password.Trim(), account.Salt.Trim()));

            #endregion

            #region 点击登录进行登录操作
            //Actions tologinAction = new Actions(driver);

            //tologinAction.Click(realLoginBtn).Build().Perform(); // 点击成功登录

            driver = driver.LeftClickElement(@"/html/body//a[@id='loginAction']");
            #endregion

            // 有可能会出现有问题的情况,需要手机验证
            // 应该可以通过Cookie获取只有登录后才会有的键值对进行判断

            /// 下面的代码会一直执行达不到等待三秒的效果，只有在while语句中添加Sleep才可以。
            //while (new WebDriverWait(driver, TimeSpan.FromSeconds(3)).Until<bool>(div=> {
            //    var element = div.Manage().Cookies.GetCookieNamed("get");
            //    return element != null ? false : true;
            //}))
            //{
            //    driver.ExecuteScript(@"console.info('等待3秒后，查看cookie')");
            //}

            #region CheckExistsCookieName  确认CookiesName，间接说明你已经登录
            //var doWait = true;
            //var WaitTime = 0;
            //while (doWait)
            //{
            //    if (WaitTime >= 10 && WaitTime % 10 == 0)
            //    {
            //        _emailManagerService.ACMEmailAutoWarn("用户编号：" + user.ID + ",在执行新浪网登陆操作时出现意外请手动处理");
            //        // 这个地方出错之后不能直接返回，需要先挂起线程等待用户手动处理
            //        // 下面的Console 无法在 Windows Service 环境下执行，可以改用数据库字段进行判断。
            //        Console.Write("程序已暂停等待用户手动处理,按任意键继续...");
            //        Console.ReadKey();
            //        Console.WriteLine("继续进行自动处理操作");
            //    }
            //    Thread.Sleep(3000);
            //    try
            //    {
            //        doWait = new WebDriverWait(driver, TimeSpan.FromSeconds(2))
            //            .Until<bool>(div =>
            //            {
            //                driver.ExecuteScript("console.info('等待3秒后，查看cookie')");
            //                var element = div.Manage().Cookies.GetCookieNamed("SUB"); //  Cookies 中的SUB字段会在登录之后出现
            //                return element != null ? false : true;
            //            });
            //    }
            //    catch
            //    {
            //        // 防止这个等待超时导致错误，可以使用try catch 处理错误
            //        driver.ExecuteScript("console.info('{0}')", "已经出现了一次等待超时");
            //    }
            //    WaitTime++;
            //}
            bool HasLogin;
            driver = driver.CheckExistsCookieName("SUB", out HasLogin,false, (seconds, url) =>
            {
                if(seconds==100)
                    _emailManagerService.ACMEmailAutoWarn("用户：" + account.NickName + ",在执行新浪网登陆操作时出现意外请手动处理。/n/t "
                        +"停在了Url："+url);
            });

            #endregion

            #region CurrentDomainGetCookies 或取当前站点已登录时cookies
            //// 如果发现成功登录了就需要重新存储Cookie
            //var loginedCookie = JsonConvert.SerializeObject(driver.Manage().Cookies.AllCookies);
            //_appAccountListBlock.UpdateTheAccountCookie(user.ID, loginedCookie);
            //driver.ExecuteScript("console.info('{0}')", "已经成功登录了系统");
            string allcookies;
            driver = driver.CurrentDomainGetCookies(out allcookies);
            // _appAccountListBlock.UpdateTheAccountCookie(account.ID, allcookies);
            #endregion

            driver.Quit(); // 退出driver



            return true;
        }







    }
}
