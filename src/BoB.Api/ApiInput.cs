using System;
using System.Collections.Generic;
using System.Text;

namespace BoB.Api
{
    public class ApiInput<In> : IApiInput
        where In:class
    {
        /// <summary>
        /// 操作人
        /// </summary>
        public Guid Opreator { get; set; }

        /// <summary>
        /// 方法名
        /// </summary>
        public string FuncName { get; set; }


        public In Data { get; set; }
    }
}
