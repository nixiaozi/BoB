using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoB.EmailManager
{
    public class WangYi126EmailSendConfig : ISendMailConfig
    {
        public string LoginUsername { get; set; }

        public string LoginPassword { get; set; }

        public string SendNickName { get; set; }

        public string SendMailAddress
        {
            get { return LoginUsername; }
        }

        public string ConnectedServer => "smtp.126.com"; // { get { return "smtp.126.com"; } }

        public int ConnectedPort { get { return 465; } }

        public bool ConnectedIsSecurity { get { return true; } }

        public string EmailSubject { get; set; }

        public TextPart EmailContent { get; set; }

        public string ReceiverNickName { get; set; }

        public string ReveiverMailAddress { get; set; }
    }
}
