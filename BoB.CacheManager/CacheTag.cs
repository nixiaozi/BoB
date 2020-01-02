using ExtendAndHelper.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoB.CacheManager
{
    public enum CacheTag
    {
        [Tag("基础配置缓存")]
        BoBConfig=1,

        [Tag("临时数据缓存")]
        Temp=1000,
    }
}
