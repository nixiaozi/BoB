using ExtendAndHelper.Extends;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExtendAndHelper.Utilties
{
    public static class PathHelper
    {
        private static string _AppRootPath = "";

        public static string GetAppRootPath()
        {
            if (string.IsNullOrEmpty(_AppRootPath))
            {
                _AppRootPath = System.Reflection.Assembly.GetEntryAssembly().GetAssemblyRoot();
            }

            return _AppRootPath;
        }

    }
}
