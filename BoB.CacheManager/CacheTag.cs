using ExtendAndHelper.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoB.CacheManager
{
    public enum CacheTag
    {
        [Tag("")]
        BoBConfig=1,

        Temp=1000,
    }
}
