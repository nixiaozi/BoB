using System;
using System.Collections.Generic;
using System.Text;

namespace BoB.BoBLogger
{
    public interface IBoBLogService
    {
        /// <summary>
        /// 记录冗余的跟踪信息（Trace）
        /// </summary>
        public void Verbose(string str,string moduleLay="",string moduleName="",string methodName="",int ErrorCode=0,Exception exception=null);

        /// <summary>
        /// 记录调试信息
        /// </summary>
        /// <param name="str"></param>
        public void Debug(string str, string moduleLay = "", string moduleName = "", string methodName = "", int ErrorCode = 0, Exception exception = null);

        /// <summary>
        /// 记录系统发生的事情，有用信息
        /// </summary>
        /// <param name="str"></param>
        public void Information(string str, string moduleLay = "", string moduleName = "", string methodName = "", int ErrorCode = 0, Exception exception = null);

        /// <summary>
        /// 记录警告信息（值得关注的信息）
        /// </summary>
        /// <param name="str"></param>
        public void Warning(string str, string moduleLay = "", string moduleName = "", string methodName = "", int ErrorCode = 0, Exception exception = null);

        /// <summary>
        /// 记录系统运行出现的错误
        /// </summary>
        /// <param name="str"></param>
        public void Error(string str, string moduleLay = "", string moduleName = "", string methodName = "", int ErrorCode = 0, Exception exception = null);

        /// <summary>
        /// 记录系统运行出现的致命错误
        /// </summary>
        /// <param name="str"></param>
        public void Fatal(string str, string moduleLay = "", string moduleName = "", string methodName = "", int ErrorCode = 0, Exception exception = null);

    }
}
