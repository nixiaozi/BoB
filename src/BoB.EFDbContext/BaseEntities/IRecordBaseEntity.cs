using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BoB.EFDbContext
{
    public interface IRecordBaseEntity<TKey> : IBaseEntity<TKey> where TKey : IComparable
    {
        public DateTime CreateTime { get; set; }

        public DateTime ModifiedTime { get; set; }

        [Display(Name = "数据当前版本")]
        public byte[] Version { get; set; }

    }
}
