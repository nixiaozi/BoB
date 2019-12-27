using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace ExtendAndHelper
{
    public static class CustomerEnvironmentExtensions
    {
        public static bool IsLeo(this IWebHostEnvironment env)
        {
            return env.EnvironmentName == CustomEnvironment.Leo;

        }
        

    }
}
