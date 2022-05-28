using BoB.BoBConfiguration;
using BoB.ExtendAndHelper.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ACM.MongoDB
{
    public static class BoBConfiguration
    {
        static BoBConfiguration()
        {
            BaseBoBConfiguration.Init(Assembly.GetExecutingAssembly());
        }


        [WriteAble]
        public static readonly string ConnectionStr = " mongodb://127.0.0.1:27017/?compressors=disabled&gssapiServiceName=mongodb";

        [WriteAble]
        public static readonly string DatabaseName = "BrowerRecord";

        [WriteAble]
        public static readonly string TaoBaoBrowseCollection = "TaoBaoRecord";


    }
}
