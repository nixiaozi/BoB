using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoB.BaseModule;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Autofac;
using BoB.BaseModule.Test.AdaptersandDecorators;
using BoB.LanguageManager;
using BoB.Work;
using BoB.BaseModule.Test.TypeInject;
using System.Diagnostics;
using ExtendAndHelper.Extends;
using BoB.BoBLogger;

namespace BoB.TestWebsite.Controller
{
    //如果使用[Route("api/[controller]")] 则只有一个url
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        public ITestService _testService;
        public ToolbarButton _toolbar;
        public IEnumerable<ToolbarButton> _toolbarButtons;
        public IEnumerable<ICommandHandler> _commandHandlers;
        public IBeginWorkBlock _beginWorkService;
        public IinjectTest _injectTest;
        public IBoBLogService _logger;

        public DefaultController(ITestService testService,ToolbarButton toolbar,
            IEnumerable<ToolbarButton> toolbarButtons, IEnumerable<ICommandHandler> commandHandlers,
            IBeginWorkBlock beginWorkService, IinjectTest injectTest, IBoBLogService logger)
        {
            _testService = testService;
            _toolbar = toolbar;
            _toolbarButtons = toolbarButtons;
            _commandHandlers = commandHandlers;
            _beginWorkService = beginWorkService;
            _injectTest = injectTest;
            _logger = logger;

            logger.Error("DefaultController Error");
        }
        
        [HttpGet]
        public void Index()
        {
            _testService.SayHello();
            _toolbar.Click();
            _toolbarButtons.ToList().ForEach(s => s.Click()); //使用保存按钮的点击事件
            _commandHandlers.ToList().ForEach(s => s.Todo());

            _beginWorkService.CheckSex();
            _injectTest.ToInjectTest();

            Debug.WriteLine((new String("2019/1/1 0:00:00")).TryConvertResult<DateTime>().ToString("yyyy-MM-dd"));

            Debug.WriteLine(BoB.BoBConfiguration.BaseBoBConfiguration.Test);
            Debug.WriteLine(BoB.BoBConfiguration.BaseBoBConfiguration.CurrentTime.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss"));
            Debug.WriteLine(BoB.CacheManager.BoBConfiguration.Test);
            Debug.WriteLine(BoB.BoBConfiguration.BaseBoBConfiguration.testPeople.Now.ToLongTimeString());
            Debug.WriteLine(BoB.BoBConfiguration.BaseBoBConfiguration.TestBool);
            Debug.WriteLine(BoB.BoBConfiguration.BaseBoBConfiguration.TestInt);
        }

        public void Now()
        {
            var item = new BeginWorkBlock();
        }

    }
}