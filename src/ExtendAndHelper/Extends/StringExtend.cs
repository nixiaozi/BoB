using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

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
