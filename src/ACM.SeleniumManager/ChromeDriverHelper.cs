using BoB.ExtendAndHelper.Extends;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;

namespace ACM.SeleniumManager
{
    public static class ChromeDriverHelper
    {
        private static Random _random;
        public static Random random
        {
            get
            {
                if (_random == null)
                {
                    _random = new Random();
                }
                return _random;
            }
        }

        public static void ExitDriver(this ChromeDriver driver)
        {
            driver.Close();
            driver.Quit();
        }

        public static ChromeDriver InitDriver(string localstr=null)
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("ignore-certificate-errors");
            chromeOptions.AddArgument("--ignore-ssl-errors");
            chromeOptions.AcceptInsecureCertificates = true; // 准许不安全的证书

            // 通过开关控制是否静音
            if(BoBConfiguration.SetMute)
                chromeOptions.AddArgument("--mute-audio");

            if (localstr!=null)
            {
                chromeOptions.AddArgument("user-data-dir="+localstr);
            }

            // 可修改可执行文件自定义引用路径
            if(BoBConfiguration.IsCustomizeExecutePath)
                return new ChromeDriver(System.Reflection.Assembly.GetEntryAssembly().GetAssemblyRoot(), chromeOptions);
            else
                return new ChromeDriver(chromeOptions);
        }


        public static ChromeDriver SwitchToWindow(this ChromeDriver driver,string WindowHandleStr)
        {
            driver.SwitchTo().Window(WindowHandleStr);
            return driver;
        }

        public static ChromeDriver SwitchToNewWindow(this ChromeDriver driver, CancellationToken ct)
        {
            bool WindowsThanOne;
            HandleCheck(driver, out WindowsThanOne, true, (url) =>
            {
                return new WebDriverWait(driver, TimeSpan.FromSeconds(3))
                    .Until<bool>(div =>
                    {
                        return div.WindowHandles.Count > 1;
                    });
            }, null, ct);

            if (WindowsThanOne)
            {
                driver.Close();
                driver.SwitchTo().Window(driver.WindowHandles[0]);
                //foreach (string window in driver.WindowHandles)
                //{
                //    if (originalWindow != window)
                //    {
                //        driver.SwitchTo().Window(window);
                //        break;
                //    }
                //}
            }

            return driver;

        }


        public static ChromeDriver PrintBrowserLog(this ChromeDriver driver, string logText)
        {
            driver.ExecuteScript("console.info('{"+ logText + "}')");
            return driver;
        }

        public static ChromeDriver ExecuteAjaxPost(this ChromeDriver driver,string url,string dataType,string data,string[] extendData)
        {
            List<string> resultData = new List<string>();
            resultData.Add(@"type:'POST'");
            resultData.Add(@"url:'" + url+"'");
            resultData.Add(@"dataType:'" + dataType+"'");
            resultData.Add(@"data:{
                "+data+@"
            }");
            resultData.AddRange(extendData);

            string execStr = @"$.ajax({
                "+string.Join(",",resultData)+@"
            })";

            // 执行JS脚本
            driver.ExecuteScript(execStr);


            return driver;
        }

        /// <summary>
        /// 添加自定义定位
        /// </summary>
        /// <param name="latitude">纬度</param>
        /// <param name="longitude">经度</param>
        /// <param name="accuracy">精确度</param>
        /// <returns></returns>
        public static ChromeDriver CustomLocation(this ChromeDriver driver, double latitude,double longitude,int accuracy = 1)
        {
            Dictionary<string, object> coordinates = new Dictionary<string, object>
            {
                {"latitude", latitude },
                {"longitude", longitude },
                {"accuracy", accuracy }
            };

            driver.ExecuteChromeCommand("Emulation.setGeolocationOverride", coordinates);
            return driver;
        }


        public static ChromeDriver ResizeWindow(this ChromeDriver driver,int width,int height)
        {
            driver.Manage().Window.Size = new Size(width, height);
            return driver;
        }

        public static ChromeDriver BrowserToUrl(this ChromeDriver driver,string NavigationUrl,
            Action<int,string> errorAction=null, CancellationToken? ct = null)
        {
            return driver.HandleError(url =>
            {
                driver.Navigate().GoToUrl(NavigationUrl);
            }, errorAction,ct);
            #region Commited code
            //var waitfor = true;
            //var waitSecond = 10; // 每十秒后再执行一次
            //while (waitfor)
            //{
            //    try
            //    {
            //        driver.Navigate().GoToUrl(url);
            //        waitfor = false;
            //    }
            //    catch
            //    {
            //        waitSecond = waitSecond +10 < 300 ? waitSecond +10 : 300;
            //        if (errorAction != null)
            //        {
            //            errorAction.Invoke(waitSecond, url);
            //        }
            //        else
            //        {
            //            Console.WriteLine("访问页面URL:" + url + "时出现错误，等待" + waitSecond + "秒后重试");
            //        }

            //        Thread.Sleep(waitSecond*1000);
            //        waitfor = true;
            //    }
            //}

            //return driver;
            #endregion
        }

        #region 直接setCookies的方式重写cookies 违反cookies的安全方式，不能达到效果
        //public static ChromeDriver CurrentDomainSetCookies(this ChromeDriver driver,
        //    string CookiesStr, Action<int, string> errorAction=null)
        //{
        //    return driver.HandleError(url =>
        //    {
        //        driver.Manage().Cookies.DeleteAllCookies(); // 首先清除所有cookies
        //        var cookieCollection = CookiesStr == null ? null : JsonConvert.DeserializeObject<List<Dictionary<string,string>>>(CookiesStr);
        //        if (cookieCollection != null)
        //        {
        //            cookieCollection.ForEach(s => /*driver.Manage().Cookies.AddCookie(s)*/{ 
        //                foreach(KeyValuePair<string,string> keyValuePair in s)
        //                {
        //                    driver.Manage().Cookies.AddCookie(new Cookie(keyValuePair.Key, keyValuePair.Value));
        //                }
        //            });
        //        }

        //    }, errorAction);
        //}

        // 检查元素可用性【用来进行判断的依据】
        #endregion

        public static ChromeDriver CheckElementUseable(this ChromeDriver driver,string xpath, out bool CheckResult, bool IsAllowFalse = false,
            Action<int, string> errorAction = null, CancellationToken? ct = null)
        {
            return driver.HandleCheck(out CheckResult, IsAllowFalse, (url) =>
            {
                return new WebDriverWait(driver, TimeSpan.FromSeconds(3))
                        .Until<bool>(div =>
                        {
                            IWebElement templateEle = driver.FindElement(By.XPath(xpath));
                            return templateEle.Displayed && templateEle.Enabled ? true : false;
                        });
            }, errorAction,ct);
        }

        public static ChromeDriver CheckElementIsShow(this ChromeDriver driver, string xpath, out bool CheckResult, bool IsAllowFalse = false,
            Action<int, string> errorAction = null, CancellationToken? ct = null)
        {
            return driver.HandleCheck(out CheckResult, IsAllowFalse, (url) =>
            {
                return new WebDriverWait(driver, TimeSpan.FromSeconds(3))
                        .Until<bool>(div =>
                        {
                            IWebElement templateEle = driver.FindElement(By.XPath(xpath));
                            return templateEle.Displayed ? true : false;
                        });
            }, errorAction, ct);
        }

        public static ChromeDriver CheckElementIsExists(this ChromeDriver driver, string xpath, out bool CheckResult, bool IsAllowFalse = false,
            Action<int, string> errorAction = null, CancellationToken? ct = null)
        {
            return driver.HandleCheck(out CheckResult, IsAllowFalse, (url) =>
            {
                return new WebDriverWait(driver, TimeSpan.FromSeconds(3))
                        .Until<bool>(div =>
                        {
                            IWebElement templateEle = driver.FindElement(By.XPath(xpath));
                            return templateEle!=null? true : false;
                        });
            }, errorAction, ct);
        }


        // 点击元素
        public static ChromeDriver LeftClickElement(this ChromeDriver driver,string xpath,
            Action<int, string> errorAction = null, CancellationToken? ct = null)
        {
            return driver.HandleError((url) =>
            {
                IWebElement element = new WebDriverWait(driver, TimeSpan.FromSeconds(2))
                        .Until<IWebElement>(div =>
                        {
                            IWebElement templateEle = driver.FindElement(By.XPath(xpath));
                            return templateEle.Displayed && templateEle.Enabled ? templateEle : null;
                        });
                Actions newAction = new Actions(driver);
                newAction.Click(element).Build().Perform();
            }, errorAction,ct);
        }


        // 输入内容[模拟人的输入]
        public static ChromeDriver InputContext(this ChromeDriver driver,string xpath,string inputStr, 
            Action<int, string> errorAction = null, CancellationToken? ct = null)
        {
            return driver.HandleError((url) => {
                var element= driver.FindElementByXPath(xpath);
                var inputText = inputStr.Trim();
                for (var i = 0; i < inputText.Length; i++)
                {
                    Thread.Sleep(random.Next(360, 877));
                    element.SendKeys(inputText[i].ToString());
                }


            }, errorAction,ct);
        }

        // 检查Cookie
        public static ChromeDriver CheckExistsCookieName(this ChromeDriver driver, string cookieName, out bool CheckResult, bool IsAllowFalse = false,
            Action<int, string> errorAction = null, CancellationToken? ct = null)
        {
            return driver.HandleCheck(out CheckResult, IsAllowFalse, (url) =>
            {
                return new WebDriverWait(driver, TimeSpan.FromSeconds(2))
                        .Until<bool>(div =>
                        {
                            var element = div.Manage().Cookies.GetCookieNamed(cookieName); 
                            return element != null ? true : false;
                        });
            }, errorAction,ct);
        }


        // 获取cookie
        public static ChromeDriver CurrentDomainGetCookies(this ChromeDriver driver,out string allCookies,
            Action<int, string> errorAction = null, CancellationToken? ct = null)
        {
            return driver.HandleGetContext<string>(out allCookies, (url) =>
            {
                return JsonConvert.SerializeObject(driver.Manage().Cookies.AllCookies);
            }, errorAction,ct);
        }



        public static ChromeDriver HandleGetContext<T>(this ChromeDriver driver,out T result,
            Func<string, T> getAction = null, Action<int, string> errorAction = null, CancellationToken? ct = null) 
        {
            result = default(T);
            var url = driver.Url;
            var isWait = true;
            var waitSeconds = 0;
            var perWait = 10;
            while (isWait)
            {
                if(ct.HasValue && ct.Value.IsCancellationRequested)
                {
                    ct.Value.ThrowIfCancellationRequested(); //抛出 RequestCancelled 异常，强行中断循环
                }
                try
                {
                    if (getAction != null)
                    {
                        result = getAction(url);
                    }
                    driver.PrintBrowserLog("HandleGetContext 访问页面URL:" + url + "，并正常返回数据。");
                    isWait = false;
                }
                catch (Exception ex)
                {
                    waitSeconds = waitSeconds + perWait;
                    if (errorAction != null)
                    {
                        errorAction.Invoke(waitSeconds, url);
                    }
                    else
                    {
                        // Console.WriteLine("访问页面URL:" + url + "时出现错误，等待" + perWait + "秒后重试");
                        driver.PrintBrowserLog("HandleGetContext 访问页面URL:" + url + "时出现错误，等待" + perWait + "秒后重试");
                    }

                    Thread.Sleep(perWait * 1000);
                    isWait = waitSeconds < 40 ? true : false; // 最多只等待40秒
                    result = default(T);
                    driver.PrintBrowserLog("HandleGetContext 访问页面URL:" + url + "时出现错误，已自动忽略并返回初始值");
                }
            }

            return driver;
        }

        public static ChromeDriver HandleCheck(this ChromeDriver driver,out bool CheckResult,bool IsAllowFalse=false,
            Func<string,bool> checkAction=null,Action<int,string> errorAction=null, CancellationToken? ct = null)
        {
            CheckResult = false;
            var url = driver.Url;
            var isWait = true;
            var waitSeconds = 0;
            var perWait = 10;
            while (isWait)
            {
                if (ct.HasValue && ct.Value.IsCancellationRequested)
                {
                    ct.Value.ThrowIfCancellationRequested(); //抛出 RequestCancelled 异常，强行中断循环
                }
                try
                {
                    if (checkAction != null)
                    {
                        CheckResult=checkAction(url);
                    }
                    driver.PrintBrowserLog("HandleCheck 访问页面URL:" + url + "，并正常返回数据。");
                    isWait = !IsAllowFalse &&!CheckResult?true: false;
                }
                catch(Exception ex)
                {
                    waitSeconds = waitSeconds + perWait;
                    if (errorAction != null)
                    {
                        errorAction.Invoke(waitSeconds, url);
                    }
                    else
                    {
                        // Console.WriteLine("访问页面URL:" + url + "时出现错误，等待" + perWait + "秒后重试");
                        driver.PrintBrowserLog("HandleCheck 访问页面URL:" + url + "时出现错误，等待" + perWait + "秒后重试");
                    }

                    Thread.Sleep(perWait * 1000);
                    isWait = IsAllowFalse && waitSeconds >= 40?false:true; // 最多只等待40秒
                    CheckResult = false;
                    driver.PrintBrowserLog("HandleCheck 访问页面URL:" + url + "时出现错误，已自动忽略并返回初始值");
                }
            }

            return driver;
        }
        public static ChromeDriver HandleError(this ChromeDriver driver,Action<string> doAction=null,
            Action<int,string> errorAction=null, CancellationToken? ct = null)
        {
            var url = driver.Url;
            var waitfor = true;
            var waitSeconds = 0;
            var perWait = 10;
            while (waitfor)
            {
                if (ct.HasValue && ct.Value.IsCancellationRequested)
                {
                    ct.Value.ThrowIfCancellationRequested(); //抛出 RequestCancelled 异常，强行中断循环
                }
                try
                {
                    // driver.Navigate().GoToUrl(url);
                    if (doAction != null)
                    {
                        doAction(url);
                    }
                    driver.PrintBrowserLog("HandleError 访问页面URL:" + url + "，并正常返回数据。");
                    waitfor = false;
                }
                catch (Exception ex)
                {
                    waitSeconds = waitSeconds + perWait;
                    if (errorAction != null)
                    {
                        errorAction.Invoke(waitSeconds, url);
                    }
                    else
                    {
                        // Console.WriteLine("访问页面URL:" + url + "时出现错误，等待" + perWait + "秒后重试");
                        driver.PrintBrowserLog("HandleError 访问页面URL:" + url + "时出现错误，等待" + perWait + "秒后重试");
                    }

                    Thread.Sleep(perWait * 1000);
                    waitfor = true;
                }
            }

            return driver;
        }

    }
}
