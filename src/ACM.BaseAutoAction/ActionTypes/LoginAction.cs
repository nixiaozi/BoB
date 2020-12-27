using System;
using System.Collections.Generic;
using System.Text;

namespace ACM.BaseAutoAction
{
    public class LoginAction:RandomBrowse
    {
        public string LoginUrl { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
