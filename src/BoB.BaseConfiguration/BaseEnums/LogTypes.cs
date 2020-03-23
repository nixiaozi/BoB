using BoB.ExtendAndHelper.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoB.BoBConfiguration.BaseEnums
{
    public enum LogTypes
    {
        [Tag("跟踪记录")]
        Verbose,
        [Tag("调试记录")]
        Debug,
        [Tag("信息记录")]
        Information,
        [Tag("警告记录")]
        Warning,
        [Tag("错误记录")]
        Error,
        [Tag("崩溃记录")]
        Fatal
    }
}
