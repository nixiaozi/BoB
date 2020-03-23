using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BoB.BaseModule.Test.AdaptersandDecorators
{
    public class OpenCommand : ICommand
    {
        public void ToDo()
        {
            Debug.WriteLine("OpenCommand");
        }
    }
}
