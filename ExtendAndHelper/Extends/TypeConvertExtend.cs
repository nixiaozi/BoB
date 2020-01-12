using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace ExtendAndHelper.Extends
{
    public static class TypeConvertExtend
    {

        /// <summary>
        /// 尝试转换值到指定类型
        /// </summary>
        /// <param name="str"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static T TryConvertResult<T>(this string str) where T:struct
        {
                TypeConverter converter =
            TypeDescriptor.GetConverter(typeof(T));

            return (T)converter.ConvertFromString(str);
        }


    }
}
