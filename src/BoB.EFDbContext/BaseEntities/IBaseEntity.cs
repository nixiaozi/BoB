using System;
using System.Collections.Generic;
using System.Text;

namespace BoB.EFDbContext
{
    public interface IBaseEntity<TKey> 
    {
        public TKey ID { get; set; }
    }
}
