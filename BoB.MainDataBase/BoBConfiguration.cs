using BoB.BoBConfiguration;
using ExtendAndHelper.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BoB.MainDataBase
{
    public static class BoBConfiguration
    {
        static BoBConfiguration()
        {
            BaseBoBConfiguration.Init(Assembly.GetExecutingAssembly());
        }


        [WriteAble]
        public static readonly string ConnectionStr = "Data Source=.;Initial Catalog=CSNAutoEntityTest;User ID=sa;Password=As412563";

    }
}
