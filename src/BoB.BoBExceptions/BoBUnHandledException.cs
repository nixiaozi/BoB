using BoB.BoBConfiguration.BaseEnums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoB.BoBExceptions
{
    public class BoBUnHandledException:BoBException
    {

        public BoBUnHandledException(string message,Exception exception)
            :base("未知","未知","未知",message,exception,99999,BoBConfiguration.BoBUnHandledExceptionLog,LogTypes.Error)
        {

        }


    }
}
