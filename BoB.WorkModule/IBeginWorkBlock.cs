﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BoB.Work
{
    public interface IBeginWorkBlock
    {
        public void CheckWord();


        public void CheckSex();


        public void Save();


        public void Send();
    }
}