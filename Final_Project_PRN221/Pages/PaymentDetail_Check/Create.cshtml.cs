using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Final_Project_PRN221.Models;

namespace Final_Project_PRN221.Pages.PaymentDetail_Check
{
    public class CreateModel : PageModel
    {
        private readonly Final_Project_PRN221.Models.Project_PRN221Context _context;

        public CreateModel(Final_Project_PRN221.Models.Project_PRN221Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["PaymentId"] = new SelectList(_context.Payments, "PaymentId", "PaymentId");
            return Page();
        }

        [BindProperty]
        public PaymentDetail PaymentDetail { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.PaymentDetails == null || PaymentDetail == null)
            {
                return Page();
            }

            _context.PaymentDetails.Add(PaymentDetail);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
