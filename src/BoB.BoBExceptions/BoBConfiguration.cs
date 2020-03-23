using BoB.BoBConfiguration;
using BoB.ExtendAndHelper.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BoB.BoBExceptions
{
    public static class BoBConfiguration
    {
        static BoBConfiguration()
        {
            BaseBoBConfiguration.Init(Assembly.GetExecutingAssembly());
        }


        [WriteAble]
        public static readonly bool BoBExceptionLog = true;

        [WriteAble]
        public static readonly bool BoBUnHandledExceptionLog = true;

    }
}
