using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoB.BoBLogger
{
    public class DevelopSerilog
    {
        public static ILogger Configuration()
        {
            return new LoggerConfiguration()
                .WriteTo.Debug()
                .CreateLogger();
        }

    }
}
