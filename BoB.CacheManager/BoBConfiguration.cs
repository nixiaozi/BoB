using ExtendAndHelper.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoB.CacheManager
{
    public static class BoBConfiguration
    {
        [WriteAble]
        public static string Test="bbbbb";  // 可编辑的属性不能添加Readonly引用

    }
}
