using System;
using System.Collections.Generic;
using System.Text;

namespace BoB.EFDbContext
{
    public interface IOrderableEntity<TKey>:IBaseEntity<TKey> where TKey:IComparable
    {
        public int SortNo { get; set; }

    }
}
