using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoB.HelloWorldApi.Model
{
    public class GetTheUserAccountOutput
    {
        public Guid id { get; set; }
        
        public int appID { get; set; }

        public string nickName { get; set; }

        public double longitude { get; set; } // 经度

        public double latitude { get; set; } // 纬度

        public string address { get; set; }

        public DateTime createTime { get; set; }

        public string lastUrl { get; set; }
    }
}
