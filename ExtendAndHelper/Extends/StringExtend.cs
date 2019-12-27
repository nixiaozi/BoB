using System;
using System.Collections.Generic;
using System.Text;

namespace ExtendAndHelper.Extends
{
    public static class StringExtend
    {
        public static string AddBeforeStr(this string str,string addstr)
        {
            return addstr + str;
        }

    }
}
