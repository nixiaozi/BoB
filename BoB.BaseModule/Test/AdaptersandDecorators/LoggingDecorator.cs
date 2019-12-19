using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BoB.BaseModule.Test.AdaptersandDecorators
{
    public class LoggingDecorator : ICommandHandler
    {
        public void Todo()
        {
            Debug.WriteLine("LoggingDecorator");
        }
    }
}
