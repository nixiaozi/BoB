using System;
using System.Collections.Generic;
using System.Text;

namespace ExtendAndHelper.CustomAttributes
{
    /// <summary>
    /// 这个标识一个配置属性是可写的
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class WriteAbleAttribute:Attribute
    {
        public bool WriteAble { get; set; }

        public WriteAbleAttribute(bool writeAble=true)
        {
            WriteAble = writeAble;
        }
    }
}
