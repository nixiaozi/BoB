using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoB.EmailManager
{
    public class EmailManagerService : IEmailManagerService
    {

        public bool SendEmail(ISendMailConfig config)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(config.SendNickName, config.SendMailAddress)); // 添加发件人
            message.To.Add(new MailboxAddress(config.ReceiverNickName, config.ReveiverMailAddress)); // 添加收件人
            message.Subject = config.EmailSubject;  // 邮件的主体

            message.Body = config.EmailContent; // 邮件正文

            using (var client = new SmtpClient())
            {
                client.Connect(config.ConnectedServer, config.ConnectedPort, config.ConnectedIsSecurity); // 客户端要连接的 smtp服务器和端口 126 参照 http://wap.126.com/xm/static/html/126_android_2.html

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate(config.LoginUsername, config.LoginPassword); // 登录可以使用 用户名就是邮箱， 这个密码就是这个授权码。

                client.Send(message);
                client.Disconnect(true);
                return true;

            }
        }


        public bool ACMEmailAutoWarn(string WarnStr)
        {
            var config = BoBConfiguration.DoEmailWarnConfig;
            config.EmailContent = new TextPart() { Text = WarnStr };
            return SendEmail(config);
        }


    }
}
