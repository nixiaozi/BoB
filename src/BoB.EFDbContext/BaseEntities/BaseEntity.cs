using BoB.EFDbContext.Enums;
using BoB.ExtendAndHelper.Extends;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BoB.EFDbContext.BaseEntities
{
    public abstract class BaseEntity<TKey> : IBaseEntity
    {
        public BaseEntity()
        {
            if (typeof(TKey) == typeof(Guid))
                ID = Guid.NewGuid().CastTo<TKey>();
        }

        /// <summary>
        /// 主键编号
        /// </summary>
        [Key]
        [Display(Name = "ID")]
        public TKey ID { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 最后更新时间
        /// </summary>
        [Display(Name = "最后更新时间")]
        public DateTime ModifiedTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 标识数据的删除状态：0正常,1删除
        /// </summary>
        [Display(Name = "条目状态")]
        public DataStatus Status { get; set; } = DataStatus.Normal;

        /// <summary>
        /// 系统备注，系统自动添加的备注（不能用于其他目的）
        /// </summary>
        [Display(Name = "备注")]
        public string Remark { get; set; }

        /// <summary>
        /// 排序字段  可设置字段排序
        /// </summary>
        [Display(Name = "排序号")]
        public int SortNo { get; set; }

        [Display(Name = "所属的APPID")]
        public Guid AppID { get; set; }

        [Display(Name = "数据当前版本")]
        public byte[] Version { get; set; }

    }
}
