using ACM.BaseAutoAction;
using ACM.SeleniumManager;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;

namespace ACM.Bilibili
{
    public static class BilibiliHelper
    {
        public static ChromeDriver InsureUserHasLogin(this ChromeDriver driver, Guid userID, CancellationToken ct, 
            string StartUrl = null)
        {
            driver.BrowserToUrl(String.IsNullOrWhiteSpace(StartUrl) ? BoBConfiguration.HomePage : StartUrl);





            return driver;
        }


        public static ChromeDriver ToSomeTargetPage(this ChromeDriver driver,CancellationToken ct, ViewAction viewTag = null)
        {
            if (viewTag != null && !string.IsNullOrWhiteSpace(viewTag.ToViewUrl))
            {
                // 以后可以添加通过不同的方式达到目标页，比如通过自己搜索，通过自己的动态或者通过栏目或首页
                driver.BrowserToUrl(viewTag.ToViewUrl, null, ct);
            }
            else
            {
                // 这里可以定义很多的固定页，通过概率计算达到某个页面

            }

            return driver;
        }

        public static ChromeDriver JustRandomBowserVideoUrl(this ChromeDriver driver,CancellationToken ct,Action<int,string> erroAction=null)
        {
            // 首先选择所有的可点击视频链接
            IList<IWebElement> allVideoElements;
            driver.HandleGetContext<IList<IWebElement>>(out allVideoElements, (url) =>
            {
                return new WebDriverWait(driver, TimeSpan.FromSeconds(3))
                        .Until<IList<IWebElement>>(div =>
                        {
                            IList<IWebElement> templateElements = driver.FindElementsByXPath(BoBConfiguration.xAllMustVideoBlocks);
                            return templateElements != null ? templateElements : default(IList<IWebElement>);
                        });
                // return  driver.FindElementsByXPath(BoBConfiguration.xAllMustVideoElements);

            },erroAction,ct);

            var availableElements = allVideoElements?.Where(s => s.Displayed && s.Enabled).ToList();

            // 然后再点击视频跳到该视频页
            if (availableElements != null && availableElements.Count > 0)
            {
                
                Random random = new Random();
                var element = availableElements[random.Next(0, availableElements.Count)].FindElement(By.XPath(BoBConfiguration.xVideoBlockVideoLink));
                //Actions newAction = new Actions(driver);
                //newAction.Click(element).Build().Perform();
                var href =  element.GetAttribute("href");
                // element.Click(); 各种直接点击的方法会报错，原因待查

                driver.BrowserToUrl(href, null, ct);


            }




            return driver;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="ct"></param>
        /// <param name="viewAction">开始时间 结束时间</param>
        /// <param name="errorAction"></param>
        /// <returns></returns>
        public static ChromeDriver ViewVideoToEnd (this ChromeDriver driver,CancellationToken ct,Action<DateTime,DateTime> viewAction=null, Action<int,string> errorAction=null)
        {
            // 首先确认页面存在视频播放器
            bool HasVideoPlay;
            driver.CheckElementIsExists(BoBConfiguration.xPageHasVideoPlay, out HasVideoPlay, false, errorAction, ct);

            IWebElement element;
            driver.HandleGetContext<IWebElement>(out element, (url) => 
            {
                return driver.FindElementByXPath(BoBConfiguration.xPageHasVideoPlay);
            }, errorAction, ct);
            // 如果判断视频没有播放，那么点击播放视频
            bool VideoIsPause =
                element.FindElement(By.XPath(BoBConfiguration.xVideoPlayArea)).GetAttribute("class").Contains(BoBConfiguration.sVideoPlayPausedClassTag);


            while (VideoIsPause)
            {
                element.FindElement(By.XPath(BoBConfiguration.xVideoPlayPlayBtn)).Click();
                VideoIsPause = driver.FindElement(By.XPath(BoBConfiguration.xVideoPlayArea))
                    .GetAttribute("class").Contains(BoBConfiguration.sVideoPlayPausedClassTag);
            }


            // 然后监控视频一直到视频播放完毕
            bool VideoToEnd = false;

            while (!VideoToEnd) // 播放完成打破循环
            {
                // 对于隐藏的元素想要获取它的text，首先必须让他显示出来
                var elapsedTimeElement = driver.FindElementByXPath(BoBConfiguration.xVideoPlayHoldTimeDisplay);
                Actions mouseOverAction = new Actions(driver);
                mouseOverAction.MoveToElement(elapsedTimeElement).Build().Perform();

                DateTime startTime;
                var startTimeText = driver.FindElementByXPath(BoBConfiguration.xVideoPlayCurrentTimeSpan).Text;
                var testStartTimeSpan = driver.FindElementByXPath(BoBConfiguration.xVideoPlayCurrentTimeSpan);
                var testName = testStartTimeSpan.GetAttribute("name");
                /// 对于隐藏的元素，如果直接使用selenium 读取它的Text属性会显示为空。



                try
                {
                    startTime = DateTime.ParseExact(startTimeText, "mm:ss", CultureInfo.InvariantCulture);
                }catch(Exception ex)
                {
                    startTime = DateTime.ParseExact(startTimeText, "HH:mm:ss", CultureInfo.InvariantCulture);
                }


                Thread.Sleep(5000); // 每五秒执行一次
                DateTime endTime;
                var endTimeText = driver.FindElementByXPath(BoBConfiguration.xVideoPlayCurrentTimeSpan).Text;
                try
                {
                    endTime = DateTime.ParseExact(endTimeText, "mm:ss", CultureInfo.InvariantCulture);
                }
                catch (Exception ex)
                {
                    endTime = DateTime.ParseExact(endTimeText, "HH:mm:ss", CultureInfo.InvariantCulture);
                }

                if (viewAction!=null)
                {
                    viewAction.Invoke(startTime, endTime);
                }

                VideoToEnd = driver.FindElement(By.XPath(BoBConfiguration.xVideoPlayArea))
                    .GetAttribute("class").Contains(BoBConfiguration.sVideoPlayEndClassTag);



            }



            return driver;
        }




        //public static ChromeDriver CheckBilibiliHaslogin(this ChromeDriver driver,CancellationToken ct,out bool  IsLogined)
        //{
        //    bool IsUnLogined;
        //    driver.CheckElementUseable(@"//div[@class='user-con logout']", out IsUnLogined, true, null, ct);
        //    IsLogined = !IsUnLogined;

        //    return driver;
        //}



        //public static ChromeDriver EnterToLoginPage(this ChromeDriver driver,CancellationToken ct)
        //{
        //    driver.LeftClickElement(@"//img[@class='logout-face']", null, ct);

        //    return driver;
        //}

        //public static ChromeDriver LoginInputUserName(this ChromeDriver driver,string UserNameStr,CancellationToken ct)
        //{
        //    // driver.InputContext(@"")

        //    return driver;

        //}


    }
}
