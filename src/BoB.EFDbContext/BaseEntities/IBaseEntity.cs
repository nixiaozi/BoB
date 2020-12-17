using BoB.EFDbContext.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoB.EFDbContext
{
    public interface IBaseEntity<TKey> where TKey:IComparable
    {
        public TKey ID { get; set; }

        public DataStatus Status { get; set; }
    }
}
