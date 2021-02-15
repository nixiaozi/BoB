using BoB.BoBConfiguration;
using BoB.ExtendAndHelper.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ACM.AutoAccountApplication
{
    public static class BoBConfiguration
    {
        static BoBConfiguration()
        {
            BaseBoBConfiguration.Init(Assembly.GetExecutingAssembly());
        }

        [WriteAble]
        public static readonly int NormalAllowParallelTaskNum = 4;

        [WriteAble]
        public static readonly int AutoCancelMinutes = 71;


    }
}
