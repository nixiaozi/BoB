using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoB.RazorWork;
using BoB.Work;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BoB.RazorTest
{
    public class CreateModel : PageModel
    {
        private ICustomerBlock _createBlock;
        private IBeginWorkBlock _beginService;

        public CreateModel(
            ICustomerBlock createBlock,
            IBeginWorkBlock beginService
            )
        {
            _createBlock = createBlock;
            _beginService = beginService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }


        [BindProperty]
        public Customer Customer { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _beginService.CheckSex();
            await _createBlock.DoCreateAsync(Customer);

            return RedirectToPage("/Index");


            // return RedirectToPage("/Index");  这个回重定向到根目录的Index页面
            // return RedirectToPage("./Index2");这个会重定向到当前目录的Index2页面
        }

    }
}