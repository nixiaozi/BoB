using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BoB.BaseModule.Test.AdaptersandDecorators
{
    public class SaveCommandHandler : ICommandHandler
    {
        public void Todo()
        {
            Debug.WriteLine("SaveCommandHandler");
        }
    }
}
