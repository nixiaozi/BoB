using BoB.EFDbContext.Enums;
using BoB.ExtendAndHelper.Extends;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BoB.EFDbContext
{
    public abstract class NormalEntity<TKey>  :BaseEntity<TKey>, IBaseEntity<TKey>,IRecordBaseEntity<TKey>, ISystemBaseEntity<TKey>
        where TKey: IComparable
    {


        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTime CreateTime { get; set; } = DateTime.Now;


        /// <summary>
        /// 创建人
        /// </summary>
        [Display(Name = "创建人")]
        public Guid CreateUser { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        [Display(Name = "最后更新时间")]
        public DateTime ModifiedTime { get; set; } = DateTime.Now;

        [Display(Name = "最后更新人")]
        public Guid ModifiedUser { get; set; }

        [Display(Name = "数据当前版本")]
        [Timestamp]
        public byte[] RowVersion { get; set; }

        /// <summary>
        /// 系统备注，系统自动添加的备注（不能用于其他目的）
        /// </summary>
        [Display(Name = "备注")]
        public string Remark { get; set; }


    }
}
