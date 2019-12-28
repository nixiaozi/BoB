using System;
using System.Collections.Generic;
using System.Text;

namespace BoB.BaseModule
{
    public interface ITestService
    {
        void SayHello();

        void SayNow();

        void Say(string str = "Default Say");
    }
}
