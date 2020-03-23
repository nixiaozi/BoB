using System;
using System.Collections.Generic;
using System.Text;

namespace BoB.ExtendAndHelper.CustomAttributes
{
    /// <summary>
    /// 这个标识一个配置属性是可写的
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class WriteAbleAttribute:Attribute
    {
        public bool WriteAble { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="writeAble">是否可写</param>
        public WriteAbleAttribute(bool writeAble=true)
        {
            WriteAble = writeAble;
        }
    }
}
