using System;
using System.Collections.Generic;
using System.Text;

namespace BoB.CacheManager
{
    public interface IContextData
    {
        public Guid UserID { get; set; }
        public string UserCode { get; set; }

        public string UserName { get; set; }

        public List<string> UserDepts { get; set; }

        public List<string> UserGroups { get; set; }

        public List<string> UserDeptsAndGroups
        {
            get;
        }


    }
}
