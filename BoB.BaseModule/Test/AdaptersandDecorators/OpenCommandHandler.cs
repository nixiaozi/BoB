using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BoB.BaseModule.Test.AdaptersandDecorators
{
    public class OpenCommandHandler : ICommandHandler
    {
        public void Todo()
        {
            Debug.WriteLine("OpenCommandHandler");
        }
    }
}
