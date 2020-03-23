using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace BoB.ExtendAndHelper.Extends
{
    public static class StringExtend
    {
        public static string AddBeforeStr(this string str,string addstr)
        {
            
            return addstr + str;
        }

    }
}
