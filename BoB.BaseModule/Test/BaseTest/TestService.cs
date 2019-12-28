﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BoB.BaseModule
{
    public class TestService : ITestService
    {
        public void SayHello()
        {
            Debug.WriteLine("Hello All of Yours!");
        }

        public void SayNow()
        {
            Debug.WriteLine("Hello Now!");
        }



        public void Say(string str="Default Say")
        {
            Debug.WriteLine(str);
        }
    }
}
