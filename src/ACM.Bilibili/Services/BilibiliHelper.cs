using ACM.BaseAutoAction;
using ACM.SeleniumManager;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;

namespace ACM.Bilibili
{
    public static class BilibiliHelper
    {


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
        public static ChromeDriver ViewVideoToEnd (this ChromeDriver driver,CancellationToken ct,Action<DateTime,DateTime,DateTime> viewAction=null, 
            Action<int,string> errorAction=null)
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
                try
                {
                    // element.FindElement(By.XPath(BoBConfiguration.xVideoPlayPlayBtn)).Click();
                    // driver.FindElement(By.XPath(BoBConfiguration.xVideoPlayPlayBtn)).Click();
                    // 解决错误的问题
                    driver.LeftClickElement(BoBConfiguration.xVideoPlayPlayBtn, null, ct);
                    VideoIsPause = driver.FindElement(By.XPath(BoBConfiguration.xVideoPlayArea))
                        .GetAttribute("class").Contains(BoBConfiguration.sVideoPlayPausedClassTag);
                }
                catch(Exception ex)
                {
                    Debug.Print("element Value:" + element.ToString());
                }
            }


            // 然后监控视频一直到视频播放完毕
            bool VideoToEnd = false;

            var totalTimeElement = driver.FindElementByXPath(BoBConfiguration.xVideoPlayHoldTimeDisplay);
            Actions totalTimeOverAction = new Actions(driver);
            totalTimeOverAction.MoveToElement(totalTimeElement).Build().Perform();
            DateTime totalTime;
            var TotalTimeStr = driver.FindElementByXPath(BoBConfiguration.xVideoPlayHoldTimeDisplay).Text;
            try
            {
                totalTime = DateTime.ParseExact(TotalTimeStr, "mm:ss", CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                totalTime = DateTime.ParseExact(TotalTimeStr, "HH:mm:ss", CultureInfo.InvariantCulture);
            }

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
                    // 出错了就使用默认值就好
                    try
                    {

                        startTime = DateTime.ParseExact(startTimeText, "HH:mm:ss", CultureInfo.InvariantCulture);
                    }
                    catch
                    {
                        startTime = new DateTime();
                    }
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
                    try
                    {
                        endTime = DateTime.ParseExact(endTimeText, "HH:mm:ss", CultureInfo.InvariantCulture);
                    }catch(Exception eex)
                    {
                        endTime = startTime.AddSeconds(5);// 实在不行可以直接增加5秒时间
                    }
                }

                if (viewAction!=null)
                {
                    viewAction.Invoke(startTime, endTime,totalTime);
                }

                VideoToEnd = driver.FindElement(By.XPath(BoBConfiguration.xVideoPlayArea))
                    .GetAttribute("class").Contains(BoBConfiguration.sVideoPlayEndClassTag);

                driver.PrintBrowserLog("现在为视频播放的开始时间:" + startTimeText + "; 结束时间为:" + endTimeText);

            }



            return driver;
        }

        
        //$.ajax({
        //type: "POST",
        //    url: "https://api.bilibili.com/x/v2/reply/action",
        //    dataType: "json",
        //    data:
        //    {
        //    oid: "671737528",
        //        type: 1,
        //        rpid: "4132238479",
        //        action: 1,
        //        ordering: "heat",
        //        jsonp: "jsonp"
        //    },
        //    xhrFields:
        //    {
        //    withCredentials: !0
        //    }
        //})
        // Comment like test!

        public static ChromeDriver CommentLike(this ChromeDriver driver,string avString,string commentID)
        {
            List<string> dataList = new List<string>
            {
                "oid: '"+avString+"'",
                "type: 1",
                "rpid:'"+commentID+"'",
                "action: 1",
                "ordering:'heat'",
                "jsonp:'jsonp'"
            };

            List<string> extendList = new List<string>
            {
                @"xhrFields: {
                    withCredentials: !0
                }"
            };

            driver.ExecuteAjaxPost(
                "https://api.bilibili.com/x/v2/reply/action",
                "json",
                string.Join(",",dataList.ToArray()),
                extendList.ToArray());

            return driver;
        }


        public static string GetVideoAVstring(this ChromeDriver driver, CancellationToken ct)
        {
            string avString;
            driver.HandleGetContext<string>(out avString, (url) =>
            {
                IWebElement element = new WebDriverWait(driver, TimeSpan.FromSeconds(3))
                        .Until<IWebElement>(div =>
                        {
                            IWebElement templateElements = driver.FindElementByXPath(BoBConfiguration.xVideoAVMeta);
                            return templateElements;
                        });

                string avUrl= element.GetAttribute("content");

                var startIndex = avUrl.LastIndexOf("av") + 2;
                var endIndex = avUrl.Length - 1;
                var length = endIndex - startIndex;

                return avUrl.Substring(startIndex, length);


            }, null, ct);

            return avString;
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
