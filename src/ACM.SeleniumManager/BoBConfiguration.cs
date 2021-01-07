using BoB.BoBConfiguration;
using System.Reflection;

namespace ACM.SeleniumManager
{
    public static class BoBConfiguration
    {
        static BoBConfiguration()
        {
            BaseBoBConfiguration.Init(Assembly.GetExecutingAssembly());
        }



    }
}
