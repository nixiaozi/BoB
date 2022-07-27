using System;
using System.Collections.Generic;
using System.Text;

namespace BoB.CacheManager
{
    public class ContextData:IContextData
    {
        private Guid _userID { get; set; }

        public Guid UserID
        {
            get { return _userID; }
            set { _userID = value; }
        }

        private string _userCode { get; set; }

        public string UserCode
        {
            get { return _userCode; }
            set { _userCode = value; }
        }

        private string _userName { get; set; }

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }


        private List<string> _userDepts { get; set; }=new List<string>();

        public List<string> UserDepts
        {
            get { return _userDepts; }
            set { _userDepts = value; }
        }

        private List<string> _userGroups { get; set; }=new List<string>();

        public List<string> UserGroups
        {
            get { return _userGroups; }
            set { _userGroups = value; }
        }

        public List<string> UserDeptsAndGroups
        {
            get
            {
                List<string> list = new List<string>();
                list.AddRange(this.UserGroups);
                list.AddRange(this.UserDepts);
                return list;
            }
        }


    }
}
