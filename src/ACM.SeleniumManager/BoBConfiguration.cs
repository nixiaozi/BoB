using BoB.BoBConfiguration;
using BoB.ExtendAndHelper.CustomAttributes;
using System.Reflection;

namespace ACM.SeleniumManager
{
    public static class BoBConfiguration
    {
        static BoBConfiguration()
        {
            BaseBoBConfiguration.Init(Assembly.GetExecutingAssembly());
        }

        [WriteAble]
        public readonly static bool SetMute = true;

        [WriteAble]
        public readonly static string ChromeProfilePath = @"D:\publish\chromeProfiles\";

    }
}
