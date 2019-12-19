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

        public DefaultController(ITestService testService,ToolbarButton toolbar,
            IEnumerable<ToolbarButton> toolbarButtons)
        {
            _testService = testService;
            _toolbar = toolbar;
            _toolbarButtons = toolbarButtons;
        }
        
        [HttpGet]
        public void Index()
        {
            _testService.SayHello();
            _toolbar.Click();
            _toolbarButtons.Where(s => s.CommandText == "Save File").FirstOrDefault().Click();  //使用保存按钮的点击事件
        }

    }
}