using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Final_Project_PRN221.Models;

namespace Final_Project_PRN221.Pages.PaymentDetail_Check
{
    public class DetailsModel : PageModel
    {
        private readonly Final_Project_PRN221.Models.Project_PRN221Context _context;

        public DetailsModel(Final_Project_PRN221.Models.Project_PRN221Context context)
        {
            _context = context;
        }

      public PaymentDetail PaymentDetail { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.PaymentDetails == null)
            {
                return NotFound();
            }

            var paymentdetail = await _context.PaymentDetails.FirstOrDefaultAsync(m => m.PaymentDetailId == id);
            if (paymentdetail == null)
            {
                return NotFound();
            }
            else 
            {
                PaymentDetail = paymentdetail;
            }
            return Page();
        }
    }
}
