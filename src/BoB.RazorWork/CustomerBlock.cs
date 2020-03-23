using BoB.ContainManager;
using BoB.RazorDataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoB.RazorWork
{
    public class CustomerBlock : BaseBlock, ICustomerBlock
    {
        protected override void Init()
        {
            
        }

        public async Task DoCreateAsync(Customer customer)
        {
            RazorDbContext context = new RazorDbContext();

            context.Add<Customer>(customer);
            await context.SaveChangesAsync();

        }

        public async Task<IList<Customer>> DoListAllAsync()
        {
            return await Task.Run(() =>
            {

                RazorDbContext context = new RazorDbContext();

                var t = context.Set<Customer>().ToList();

                return t;
            });
            
        }


        public async Task DoDeleteAsync(int Key)
        {
            RazorDbContext context = new RazorDbContext();

            context.Remove(context.Set<Customer>().Find(Key));

            await context.SaveChangesAsync();
        }

    }
}
