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

        [WriteAble]
        public static readonly WangYi126EmailSendConfig DoEmailWarnConfig = new WangYi126EmailSendConfig
        {
            ReveiverMailAddress = "776493846@qq.com",
            EmailSubject = "ACM 系统自动发出的警告邮件，请立即处理!",
            LoginUsername = "nixiaozi01@126.com",
            LoginPassword = "SSTQDRCILQUPBDQB",
            ReceiverNickName = "Leo",
            SendNickName = "ACM Auto",
        };

    }
}
