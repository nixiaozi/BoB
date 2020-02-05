using BoB.BoBConfiguration;
using ExtendAndHelper.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BoB.RazorDataBase
{
    public static class BoBConfiguration
    {
        static BoBConfiguration()
        {
            BaseBoBConfiguration.Init(Assembly.GetExecutingAssembly());
        }


        [WriteAble]
        public static readonly string ConnectionStr = "Data Source=.;Initial Catalog=RazorTest;User ID=sa;Password=As412563";

    }
}
