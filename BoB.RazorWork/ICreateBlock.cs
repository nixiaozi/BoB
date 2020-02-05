using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoB.RazorWork
{
    public interface ICreateBlock
    {
        public Task DoCreateAsync(Customer customer);

    }
}
