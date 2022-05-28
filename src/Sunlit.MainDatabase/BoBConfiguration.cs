using BoB.BoBConfiguration;
using BoB.ExtendAndHelper.CustomAttributes;
using System.Reflection;

namespace Sunlit.MainDatabase
{
    public static class BoBConfiguration
    {
        static BoBConfiguration()
        {
            BaseBoBConfiguration.Init(Assembly.GetExecutingAssembly());
        }

        //使用InMemory数据库不需要
        //[WriteAble]
        //public static readonly string ConnectionStr = "Data Source=.;Initial Catalog=CSNAutoEntityTest;User ID=sa;Password=As412563";

        [WriteAble]
        public static readonly string ConnectionStr = "Data Source=.;Initial Catalog=SBMS;User ID=sa;Password=As412563";

    }
}
