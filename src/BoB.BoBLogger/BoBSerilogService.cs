using BoB.ExtendAndHelper.Utilties;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BoB.BoBLogger
{
    public class BoBSerilogService : IBoBLogService
    {
        private ILogger logger = null;

        public BoBSerilogService()
        {
            var rootPath = PathHelper.GetAppRootPath();
            if (!Directory.Exists(rootPath + BoBConfiguration.LogFolder))
            {
                Directory.CreateDirectory(rootPath + BoBConfiguration.LogFolder);
            }

            var filePath = rootPath + BoBConfiguration.LogFolder + "log-.txt";

            logger = new LoggerConfiguration()
            .WriteTo.Debug()
            .WriteTo.File(filePath, 
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                rollingInterval: RollingInterval.Day)
            .CreateLogger();
        }

        public void Debug(string str, string moduleLay = "", string moduleName = "", string methodName = "", int ErrorCode = 0, Exception exception = null)
        {
            logger.Debug(str, exception);
        }

        public void Error(string str, string moduleLay = "", string moduleName = "", string methodName = "", int ErrorCode = 0, Exception exception = null)
        {
            logger.Error(str, exception);
        }

        public void Fatal(string str, string moduleLay = "", string moduleName = "", string methodName = "", int ErrorCode = 0, Exception exception = null)
        {
            logger.Fatal(str, exception);
        }

        public void Information(string str, string moduleLay = "", string moduleName = "", string methodName = "", int ErrorCode = 0, Exception exception = null)
        {
            logger.Information(str, exception);
        }

        public void Verbose(string str, string moduleLay = "", string moduleName = "", string methodName = "", int ErrorCode = 0, Exception exception = null)
        {
            logger.Verbose(str, exception);
        }

        public void Warning(string str, string moduleLay = "", string moduleName = "", string methodName = "", int ErrorCode = 0, Exception exception = null)
        {
            logger.Warning(str, exception);
        }
    }
}
