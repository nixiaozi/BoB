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
        // public ToolbarButton _toolbar;
        public DefaultController(ITestService testService)
        {
            _testService = testService;
        }
        
        [HttpGet]
        public void Index()
        {
            _testService.SayHello();

        }

    }
}