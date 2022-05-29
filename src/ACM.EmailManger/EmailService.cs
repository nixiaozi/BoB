using Autofac;
using BoB.BoBContainManager;
using BoB.EmailManager;
using MimeKit;

namespace ACM.EmailManager
{
    public class EmailService : InitBlockService, IEmailService
    {
        private IEmailManagerService _emailManagerService;

        public EmailService()
        {
            _emailManagerService = CurrentServiceContainer.Resolve<IEmailManagerService>();
        }

        public bool ACMEmailAutoWarn(string WarnStr)
        {
            var config = BoBConfiguration.DoEmailWarnConfig;
            config.EmailContent = new TextPart() { Text = WarnStr };
            return _emailManagerService.SendEmail(config);
        }

    }
}
