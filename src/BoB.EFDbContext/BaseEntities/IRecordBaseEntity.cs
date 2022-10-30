using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BoB.EFDbContext
{
    public interface IRecordBaseEntity<TKey> : IBaseEntity<TKey> where TKey : IComparable
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public Guid CreateUser { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifiedTime { get; set; }

        public Guid ModifiedUser { get; set; }

        [Display(Name = "数据当前版本")]
        [Timestamp]
        public byte[] RowVersion { get; set; }

    }
}
