using Autofac;
using BoB.ContainManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sunlit.BussinessAssociaterEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sunlit.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BussinessAssociaterController : SunlitBaseController
    {
        private IBussinessAssociaterBlock _bussinessAssociaterBlock;


        public BussinessAssociaterController()  // 构造方法 不带参数
        {
            _bussinessAssociaterBlock = BoBContainer.ServiceContainer.Resolve<IBussinessAssociaterBlock>();

        }


        [HttpGet]
        public  ActionResult<string> GetAllAssociater(int index=0)
        {
            if (index == 0)
            {
                var data = _bussinessAssociaterBlock.AsyncGetList(s=>1==1);


                return Ok("你将获取所有页面！");
            }
            else
            {
                return Ok("你将获取页面"+ index);
            }
        }


    }
}
