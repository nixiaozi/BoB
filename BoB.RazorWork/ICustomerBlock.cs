using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoB.RazorWork
{
    public interface ICustomerBlock
    {
        public Task DoCreateAsync(Customer customer);

        public Task<IList<Customer>> DoListAllAsync();

        public Task DoDeleteAsync(int Key);

    }
}
