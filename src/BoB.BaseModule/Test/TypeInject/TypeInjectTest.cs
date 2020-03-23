using System;
using System.Collections.Generic;
using System.Text;

namespace BoB.BaseModule.Test.TypeInject
{
    public class TypeInjectTest : IinjectTest
    {
        private ITodo _todo;

        public TypeInjectTest(ITodo todo)
        {
            _todo = todo;
        }

        public void ToInjectTest()
        {
            _todo.Doing();
        }
    }
}
