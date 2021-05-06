using System;
using System.Collections.Generic;
using System.Text;

namespace BoB.EFDbContext
{
    public interface ISystemBaseEntity<TKey> : IBaseEntity<TKey> where TKey : IComparable
    {
        /// <summary>
        /// 系统备注，系统自动添加的备注
        /// </summary>
        public string Remark { get; set; }

    }
}
