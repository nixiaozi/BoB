using System;
using System.Collections.Generic;
using System.Text;

namespace ACM.BaseAutoAction
{
    public class ViewAction:RandomBrowse
    {
        // BV号
        public string IdentityID { get; set; }

        public string ToViewUrl { get; set; }

        public string[] ContextTag { get; set; }
    } 
}
