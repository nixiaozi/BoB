using BoB.BoBConfiguration;
using BoB.ExtendAndHelper.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ACM.SinaChina
{
    public static class BoBConfiguration
    {
        static BoBConfiguration()
        {
            BaseBoBConfiguration.Init(Assembly.GetExecutingAssembly());
        }


        // [WriteAble]
        public static readonly string HomePage = "http://www.sina.cn";

        public static readonly int AppID = 4;

    }
}
