using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoB.HelloWorldApi.Model
{
    public class ACMAddAccountModel
    {
        public int appID { get; set; }

        public string nickName { get; set; }

        public string password { get; set; }

        public string address { get; set; }

        public Guid userID { get; set; }
    }
}
