using BoB.ExtendAndHelper.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoB.BoBConfiguration.BaseEnums
{
    /// <summary>
    /// 缓存的标识，所有使用缓存的项目必须使用这里注册的标识
    /// </summary>
    public enum CacheTag
    {
        [Tag("框架基础缓存")]
        BoBConfig=1,
        [Tag("语言翻译缓存")]
        BoBLangService =2,

        [Tag("其他类型缓存")]
        Others =10000,
    }
}
