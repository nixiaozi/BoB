using System.Collections.Generic;
using System.Threading.Tasks;
using BoB.RazorWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BoB.RazorTest.Pages
{
    public class IndexModel : PageModel
    {
        private ICustomerBlock _customerservice;

        public IndexModel(ICustomerBlock customerservice)
        {
            _customerservice = customerservice;
        }

        public IList<Customer> Customer { get; set; }


        public async Task OnGetAsync()
        {
            Customer = await _customerservice.DoListAllAsync();
        }


        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            

            await _customerservice.DoDeleteAsync(id);

            return RedirectToPage();
        }


    }
}
