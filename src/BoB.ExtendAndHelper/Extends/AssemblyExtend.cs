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
        public static string GetAssemblyCodeRoot(this Assembly assembly)
        {
            // CodeBase 表示编译时的assembly 存在的真实位置，这个主要是为了给调试提供依据，真实环境中不要使用
            var exePath = Path.GetDirectoryName(assembly.CodeBase);
            Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
            var appRoot = appPathMatcher.Match(exePath).Value;
            return appRoot;
        }


        public static string GetAssemblyRoot(this Assembly assembly)
        {
            return Path.GetDirectoryName(assembly.Location);
        }

    }
}
