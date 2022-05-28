using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BoB.ExtendAndHelper.Extends
{
    public static class EnumExtend
    {
        public static string DisplayName(this Enum theEnum)
        {
            MemberInfo[] memberInfo= theEnum.GetType().GetMember(theEnum.ToString());
            // memberInfo.ToList().First(s => s.Name == theEnum.ToString());

            var result = memberInfo[0].GetCustomAttribute<DisplayAttribute>();

            return result==null?theEnum.ToString():result.Name;

        }

        public static int GetValue(this Enum theEnum)
        {



            return 0;
        }
    }
}
