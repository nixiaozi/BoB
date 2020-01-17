using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoB.BoBLogger
{
    public class BoBSerilogService : IBoBLogService
    {
        private ILogger logger = null;

        public BoBSerilogService()
        {
            logger= new LoggerConfiguration()
                .WriteTo.Debug()
                .CreateLogger();
        }

        public void Debug(string str)
        {
            logger.Debug(str);
        }

        public void Error(string str)
        {
            logger.Error(str);
        }

        public void Fatal(string str)
        {
            logger.Fatal(str);
        }

        public void Information(string str)
        {
            logger.Information(str);
        }

        public void Verbose(string str)
        {
            logger.Verbose(str);
        }

        public void Warning(string str)
        {
            logger.Warning(str);
        }
    }
}
