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
    [Route("[controller]/[action]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        private IPeopleBlock _peopleBlock;
        private IWorldActionBlock _worldActionBlock;

        public DefaultController(IPeopleBlock peopleBlock, IWorldActionBlock worldActionBlock)
        {
            _peopleBlock = peopleBlock;
            _worldActionBlock = worldActionBlock;
        }


        public string GetIndex()
        {
            return "Hello";
        }


        public ActionResult<ApiResult<int>> AddPeople(People people)
        {
            ApiResult<int> result = new ApiResult<int>(false);

            result.Data= _peopleBlock.Add(people);
            result.Success = true;

            return Ok(result);
        }


    }
}