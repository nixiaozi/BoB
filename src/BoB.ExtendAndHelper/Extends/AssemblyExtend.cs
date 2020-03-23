using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace BoB.ExtendAndHelper.Extends
{
    public static class AssemblyExtend
    {
        public static string GetAssemblyRoot(this Assembly assembly)
        {
            var exePath = Path.GetDirectoryName(assembly.CodeBase);
            Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
            var appRoot = appPathMatcher.Match(exePath).Value;
            return appRoot;
        }
    }
}
