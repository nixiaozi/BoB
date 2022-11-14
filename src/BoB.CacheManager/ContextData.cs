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


        private Guid _userCompanyID { get; set; }

        public Guid UserCompanyID
        {
            get { return _userCompanyID; }
            set { this._userCompanyID = value; }
        }

        private string _userCompanyCode { get; set; }

        public string UserCompanyCode
        {
            get { return _userCompanyCode; }
            set { this._userCompanyCode = value; }
        }

        private bool _isCompanyOwner { get; set; }

        public bool IsCompanyOwner
        {
            get { return _isCompanyOwner; }
            set { this._isCompanyOwner = value; }
        }

        private int _userRange { get; set; }

        public int UserRange
        {
            get { return _userRange; }
            set { _userRange = value; }
        }

        private string _UserCompanyName;
        public string UserCompanyName
        {
            get { return _UserCompanyName; }
            set { this._UserCompanyName = value; }
        }

        private string _BaseUrl;
        public string BaseUrl
        {
            get { return _BaseUrl; }
            set { this._BaseUrl = value; }
        }

        private Dictionary<string, string> _HeaderDic;
        public Dictionary<string, string> HeaderDic
        {
            get { return _HeaderDic; }
            set { this._HeaderDic = value; }
        }
    }
}
