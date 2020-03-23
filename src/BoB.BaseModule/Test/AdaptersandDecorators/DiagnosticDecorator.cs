using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BoB.BaseModule.Test.AdaptersandDecorators
{
    public class DiagnosticDecorator : ICommandHandler
    {
        public void Todo()
        {
            Debug.WriteLine("DiagnosticDecorator");
        }
    }
}
