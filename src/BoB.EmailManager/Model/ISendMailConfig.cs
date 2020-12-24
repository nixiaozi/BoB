using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoB.EmailManager
{
    public interface ISendMailConfig
    {

        public string LoginUsername { get; }

        public string LoginPassword { get;}

        public string SendNickName { get; }

        public string SendMailAddress { get; }

        public string ConnectedServer { get; }

        public int ConnectedPort { get; }

        public bool ConnectedIsSecurity { get; }

        public string EmailSubject { get;}

        public TextPart EmailContent { get;  }

        public string ReceiverNickName { get; }

        public string ReveiverMailAddress { get; set; }

    }
}
