using BoB.BoBConfiguration;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ACM.Bilibili
{
    public static class BoBConfiguration
    {
        static BoBConfiguration()
        {
            BaseBoBConfiguration.Init(Assembly.GetExecutingAssembly());
        }


        // [WriteAble]
        public static readonly string HomePage = "http://www.bilibili.cn";

        public static readonly int AppID = 2;

    }
}
