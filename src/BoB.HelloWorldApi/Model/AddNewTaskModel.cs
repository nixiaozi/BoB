using ACM.BaseAutoAction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoB.HelloWorldApi.Model
{
    public class AddNewTaskModel
    {
        public int appID { get; set; }

        public ACMTaskTypeEnum taskType { get; set; }

        public ACMTaskLevelEnum taskLevel { get; set; }

        public Guid userAccount { get; set; }

        public string taskParamStr { get; set; }
    }
}
