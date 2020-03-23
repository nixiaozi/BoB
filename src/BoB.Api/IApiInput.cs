using System;
using System.Collections.Generic;
using System.Text;

namespace BoB.Api
{
    public interface IApiInput
    {
        /// <summary>
        /// 操作人
        /// </summary>
        Guid Opreator { get; set; }

        /// <summary>
        /// 方法操作定义
        /// </summary>
        string FuncName { get; set; }

    }
}
