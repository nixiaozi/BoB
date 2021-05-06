using BoB.EFDbContext.Enums;
using BoB.ExtendAndHelper.Extends;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BoB.EFDbContext
{
    public abstract class FullEntity<TKey>  : NormalEntity<TKey>, IBaseEntity<TKey>,IRecordBaseEntity<TKey>,
        ISystemBaseEntity<TKey>,IOrderableEntity<TKey>
        where TKey: IComparable
    {
        /// <summary>
        /// 排序字段  可设置字段排序
        /// </summary>
        [Display(Name = "排序号")]
        public int SortNo { get; set; }

        [Display(Name = "所属的APPID")]
        public Guid AppID { get; set; }

    }
}
