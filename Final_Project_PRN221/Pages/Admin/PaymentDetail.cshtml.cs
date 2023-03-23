using Final_Project_PRN221.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Final_Project_PRN221.Pages.Admin
{
    public class PaymentDetailModel : PageModel
    {
        private readonly Project_PRN221Context _context;
        public PaymentDetailModel(Project_PRN221Context context)
        {
            _context = context;
        }
        public IActionResult OnGet(int? paymentID)
        {
            if(paymentID == null)
            {
                return RedirectToPage("/Error");
            }
            else
            {

                return Page();
            }
        }
    }
}
