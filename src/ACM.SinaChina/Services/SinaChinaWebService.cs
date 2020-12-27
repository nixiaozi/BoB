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

namespace ACM.SinaChina
{
    public class SinaChinaWebService : InitBlockService, ISinaChinaWebService
    {
        private IUserBlock _userBlock;
        private IAppAccountListBlock _appAccountListBlock;
        private IEmailManagerService _emailManagerService;
        protected override void Init()
        {
            _userBlock= CurrentServiceProvider.GetService<IUserBlock>();
            _appAccountListBlock= CurrentServiceProvider.GetService<IAppAccountListBlock>();
            _emailManagerService= CurrentServiceProvider.GetService<IEmailManagerService>();
        }

        public bool ToLogin(AppAccountList account)
        {
            var user = _userBlock.GetUserById(account.UserID);
            Debug.Print("开始自动登录");

            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("ignore-certificate-errors"); 
            chromeOptions.AddArgument("--ignore-ssl-errors");
            chromeOptions.AcceptInsecureCertificates = true; // 准许不安全的证书

            //chromeOptions.AddAdditionalCapability("ACCEPT_SSL_CERTS", true);




            var driver = new ChromeDriver(chromeOptions);
            //下面是添加自定义的地理位置信息
            Dictionary<string, object> coordinates = new Dictionary<string, object>
            {
                {"latitude", 50.2334 },
                {"longitude", 0.2334 },
                {"accuracy", 1 }
            };

            driver.ExecuteChromeCommand("Emulation.setGeolocationOverride", coordinates);




            driver.Manage().Window.Size = new Size(1024, 768);

            driver.Navigate().GoToUrl(BoBConfiguration.HomePage);



            // 需要获取用户并且进行登录
            var cookieCollection = JsonConvert.DeserializeObject<List<Cookie>>(account.Cookie);
            if(cookieCollection!=null)
                cookieCollection.ForEach(s=> driver.Manage().Cookies.AddCookie(s));


            IWebElement element=  driver.FindElementByXPath(@"//nav/a[contains(@class,'hd_s_set')]"); // 找到设置按钮
            Actions actionProvider = new Actions(driver);
            actionProvider.Click(element).Build().Perform();   // 点击设置按钮

            // 条件判断 只有未登录情况下才进行登录操作
            try
            {
                var loginBtn = new WebDriverWait(driver, TimeSpan.FromSeconds(3))
                        .Until(drv => drv.FindElement(By.Id("j_login_btn")));
                Actions loginAction = new Actions(driver); // 必须要使用新建的Actions 否则会出错

                loginAction.Click(loginBtn).Build().Perform();
            }
            catch(Exception ex)
            {
                driver.Quit(); // 用户已登录
                return true;
            }

            // 获取进行登录操作的按钮
            var realLoginBtn = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                //.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath(@"/html/body//a[@id='loginAction']"))); // ExpectedConditions is Obsolete
                .Until<IWebElement>(drv =>
                {
                    IWebElement templateEle = drv.FindElement(By.XPath(@"/html/body//a[@id='loginAction']"));
                    return templateEle.Displayed && templateEle.Enabled ? templateEle : null;
                });

            // 填写用户名和密码
            var userNameText = driver.FindElementByXPath(@"/html/body//input[@id='loginName']");
            var passwordText = driver.FindElementByXPath(@"/html/body//input[@id='loginPassword']");

            var userNameStr = user.Phone.Trim();
            Random random = new Random();
            //userNameText.SendKeys(userNameStr); //  important：正式环境下需要从数据库中获取数据
            // 模拟用户输入，不要复制
            for(var i = 0; i < userNameStr.Length; i++)
            {
                
                Thread.Sleep(random.Next(360,877));
                userNameText.SendKeys(userNameStr[i].ToString());
            }

            //passwordText.SendKeys(SecurityHelper.DecryptFromBase64(account.Password.Trim(), account.Salt.Trim()));
            var passwordStr = SecurityHelper.DecryptFromBase64(account.Password.Trim(), account.Salt.Trim());
            for(var i = 0; i < passwordStr.Length; i++)
            {
                Thread.Sleep(random.Next(360, 877));
                passwordText.SendKeys(passwordStr[i].ToString());
            }

            Actions tologinAction = new Actions(driver);

            tologinAction.Click(realLoginBtn).Build().Perform(); // 点击成功登录

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

            var doWait = true;
            var WaitTime = 0;
            while (doWait)
            {
                if (WaitTime>=10 && WaitTime %10==0)
                {
                    _emailManagerService.ACMEmailAutoWarn("用户编号："+user.ID+",在执行新浪网登陆操作时出现意外请手动处理");
                    // 这个地方出错之后不能直接返回，需要先挂起线程等待用户手动处理
                    // 下面的Console 无法在 Windows Service 环境下执行，可以改用数据库字段进行判断。
                    Console.Write("程序已暂停等待用户手动处理,按任意键继续...");
                    Console.ReadKey();
                    Console.WriteLine("继续进行自动处理操作");
                }
                Thread.Sleep(3000);
                doWait = new WebDriverWait(driver, TimeSpan.FromSeconds(2))
                    .Until<bool>(div =>
                    {
                        driver.ExecuteScript("console.info('等待3秒后，查看cookie')");
                        var element = div.Manage().Cookies.GetCookieNamed("SUB"); //  Cookies 中的SUB字段会在登录之后出现
                        return element != null ? false : true;
                    });
                WaitTime++;
            }


            // 如果发现成功登录了就需要重新存储Cookie
            var loginedCookie = JsonConvert.SerializeObject(driver.Manage().Cookies.AllCookies);
            _appAccountListBlock.UpdateTheAccountCookie(user.ID, loginedCookie);
            driver.ExecuteScript("console.info('{0}')","已经成功登录了系统");


            driver.Quit(); // 退出driver



            return true;
        }







    }
}
