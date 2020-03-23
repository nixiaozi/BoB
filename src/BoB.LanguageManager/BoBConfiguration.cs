using BoB.BoBConfiguration;
using BoB.BoBConfiguration.BaseEnums;
using BoB.ExtendAndHelper.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BoB.LanguageManager
{
    public static class BoBConfiguration
    {
        static BoBConfiguration()
        {
            BaseBoBConfiguration.Init(Assembly.GetExecutingAssembly());
        }


        [WriteAble]
        public static readonly LanguageType BaseLangType =  LanguageType.zh_cn;

        public static readonly string LangFolder = "/Lang/";

    }
}
