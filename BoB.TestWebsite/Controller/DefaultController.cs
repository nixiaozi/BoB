using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoB.BaseModule;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Autofac;
using BoB.BaseModule.Test.AdaptersandDecorators;

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

        public DefaultController(ITestService testService,ToolbarButton toolbar,
            IEnumerable<ToolbarButton> toolbarButtons, IEnumerable<ICommandHandler> commandHandlers)
        {
            _testService = testService;
            _toolbar = toolbar;
            _toolbarButtons = toolbarButtons;
            _commandHandlers = commandHandlers;
        }
        
        [HttpGet]
        public void Index()
        {
            _testService.SayHello();
            _toolbar.Click();
            _toolbarButtons.ToList().ForEach(s => s.Click()); //使用保存按钮的点击事件
            _commandHandlers.ToList().ForEach(s => s.Todo());
        }

    }
}