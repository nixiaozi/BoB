using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BoB.Work
{
    //[NotMapped]
    public class SchoolDto:School
    {
        public string Detail
        {
            get
            {
                return this.Key.ToString() +"-"+ this.Location.ToString();
            }
        }



    }
}
