using BoB.ContainManager;
using BoB.RazorDataBase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoB.RazorWork
{
    public class CreateBlock : BaseBlock, ICreateBlock
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
    }
}
