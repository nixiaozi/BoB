using BoB.BoBConfiguration;
using BoB.ExtendAndHelper.CustomAttributes;
using MimeKit;
using System;
using System.Reflection;

namespace BoB.EmailManager
{
    public static class BoBConfiguration
    {
        static BoBConfiguration()
        {
            BaseBoBConfiguration.Init(Assembly.GetExecutingAssembly());
        }
    }
}
