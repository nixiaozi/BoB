using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoB.Api;
using BoB.PeopleEntities;
using BoB.WorldAction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BoB.HelloWorldApi.Controllers
{
    /// <summary>
    /// DefaultController summary
    /// </summary>
    /// <remarks>DefaultController</remarks>
    [Route("[controller]/[action]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        private IPeopleBlock _peopleBlock;
        private IWorldActionBlock _worldActionBlock;

        /// <summary>
        /// DefaultController Init
        /// </summary>
        /// <param name="peopleBlock"></param>
        /// <param name="worldActionBlock"></param>
        public DefaultController(IPeopleBlock peopleBlock, IWorldActionBlock worldActionBlock)
        {
            _peopleBlock = peopleBlock;
            _worldActionBlock = worldActionBlock;
        }

        /// <summary>
        /// GetIndex Hello
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetIndex()
        {
            return "Hello";
        }

        /// <summary>
        /// AddPeople（添加的人）
        /// </summary>
        /// <param name="people">添加的人</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<ApiResult<int>> AddPeople(People people)
        {
            ApiResult<int> result = new ApiResult<int>(false);

            result.Data= _peopleBlock.Add(people);
            result.Success = true;

            return Ok(result);
        }

        /// <summary>
        /// 获取所有有效的People
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<ApiResult<List<People>>> AllValidatePeopleList()
        {
            ApiResult<List<People>> result = new ApiResult<List<People>>(false);

            result.Data = _peopleBlock.GetAllValidatePeople();
            result.Success = true;

            return Ok(result);
        }

        /// <summary>
        /// 所有的人都sayhello
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<ApiResult<List<string>>> AllSayHello()
        {
            ApiResult<List<string>> result = new ApiResult<List<string>>(false);

            result.Data = _worldActionBlock.AllSayHello();
            result.Success = true;

            return Ok(result);
        }

    }
}