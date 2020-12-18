using BoB.EFDbContext.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ACM.AppAccountListEntities
{
    public class AppAccountInput
    {
        public DataStatus Status { get; set; }

        public Guid UserID { get; set; }

        public int AppID { get; set; }

        public string NickName { get; set; }

        public string AppUserID { get; set; }

        public string Cookie { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public double Longitude { get; set; }

        public string Address { get; set; }

        public bool OnLine { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }
    }
}
