using BoB.EFDbContext.Enums;
using BoB.ExtendAndHelper.Extends;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BoB.EFDbContext
{
    public abstract class BaseEntity<TKey>  : IBaseEntity<TKey> 
        where TKey: IComparable
    {
        /// <summary>
        /// 主键编号
        /// </summary>
        [Key]
        [Display(Name = "ID")]
        public TKey ID { get; set; }


        /// <summary>
        /// 标识数据的删除状态：0正常,1删除
        /// </summary>
        [Display(Name = "条目状态")]
        public DataStatus Status { get; set; } = DataStatus.Normal;

    }
}
