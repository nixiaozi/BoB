using BoB.BoBConfiguration.BaseEnums;
using BoB.BoBLogger;
using BoB.ContainManager;
using System;
using Microsoft.Extensions.DependencyInjection;


namespace BoB.BoBExceptions
{
    public class BoBException:Exception
    {
        private string _ModuleLay;

        public string ModuleLay
        {
            get { return _ModuleLay; }
        }

        private string _ModuleName;

        public string ModuleName { get { return _ModuleName; } }


        private string _MethodName;

        public string MethodName { get { return _MethodName; } }


        public IBoBLogService _log;

        public BoBException(string moduleLay,string moduleName,string methodName,string message,
            Exception exception=null,int hresult=10000,bool? logOpend=null,LogTypes logTypes= LogTypes.Information)
            :base(message,exception)
        {
            _ModuleLay = moduleLay;
            _ModuleName = moduleName;
            _MethodName = methodName;
            HResult = hresult; //默认的异常编号是10000

            var _logOpend = logOpend.HasValue ? logOpend.Value : BoBConfiguration.BoBExceptionLog;

            if (_logOpend)
            {
                _log = BoBContainer.ServiceProvider.GetService<IBoBLogService>();

                switch (logTypes)
                {
                    case LogTypes.Verbose:
                        _log.Verbose(Message, _ModuleLay, _ModuleName, _MethodName, HResult, exception);
                        break;
                    case LogTypes.Debug:
                        _log.Debug(Message, _ModuleLay, _ModuleName, _MethodName, HResult, exception);
                        break;
                    case LogTypes.Information:
                        _log.Information(Message, _ModuleLay, _ModuleName, _MethodName, HResult, exception);
                        break;
                    case LogTypes.Warning:
                        _log.Warning(Message, _ModuleLay, _ModuleName, _MethodName, HResult, exception);
                        break;
                    case LogTypes.Error:
                        _log.Error(Message, _ModuleLay, _ModuleName, _MethodName, HResult, exception);
                        break;
                    case LogTypes.Fatal:
                        _log.Fatal(Message, _ModuleLay, _ModuleName, _MethodName, HResult, exception);
                        break;

                }

            }

        }

        /// <summary>
        /// 这个是抛出异常的方法
        /// </summary>
        public void Throw()
        {
            throw this;
        }

    }
}
