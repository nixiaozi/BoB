using System;
using System.Collections.Generic;
using System.Text;

namespace BoB.EmailManager
{
    public interface IEmailManagerService
    {
        public bool SendEmail(ISendMailConfig config);

        
    }
}
