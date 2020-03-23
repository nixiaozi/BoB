using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BoB.BaseModule.Test.TypeInject
{
    public class Todo : ITodo
    {
        public void Doing()
        {
            Debug.WriteLine("Doing");
        }
    }
}
