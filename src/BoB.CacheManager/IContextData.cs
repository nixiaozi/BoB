using System;
using System.Collections.Generic;
using System.Text;

namespace BoB.CacheManager
{
    public interface IContextData
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public Guid UserID { get; set; }

        /// <summary>
        /// 用户代码
        /// </summary>
        public string UserCode { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户所属的部门
        /// </summary>
        public List<string> UserDepts { get; set; }

        /// <summary>
        /// 用户所属的部门
        /// </summary>
        public List<string> UserGroups { get; set; }

        public List<string> UserDeptsAndGroups
        {
            get;
        }

        /// <summary>
        /// 用户公司编号
        /// </summary>
        public Guid UserCompanyID { get; set; }

        /// <summary>
        /// 用户公司代码
        /// </summary>
        public string UserCompanyCode { get; set; }

        /// <summary>
        /// 用户公司名称
        /// </summary>
        public string UserCompanyName { get; set; }

        /// <summary>
        /// 用户所属的公司等级
        /// </summary>
        public int UserRange { get; set; }

        /// <summary>
        /// 请求的基础URL
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// 记录可能需要使用到的请求头记录
        /// </summary>
        public Dictionary<string, string> HeaderDic { get; set; }

    }
}
