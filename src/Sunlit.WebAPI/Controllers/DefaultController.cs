using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sunlit.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DefaultController : SunlitBaseController
    {
        [HttpGet]
        public  ActionResult<string> Hello()
        {
            return Ok("你好！测试请求成功");
        }


    }
}
