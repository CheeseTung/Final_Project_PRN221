using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Final_Project_PRN221.Models;

namespace Final_Project_PRN221.Pages.PaymentDetail_Check
{
    public class EditModel : PageModel
    {
        private readonly Final_Project_PRN221.Models.Project_PRN221Context _context;

        public EditModel(Final_Project_PRN221.Models.Project_PRN221Context context)
        {
            _context = context;
        }

        [BindProperty]
        public PaymentDetail PaymentDetail { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.PaymentDetails == null)
            {
                return NotFound();
            }

            var paymentdetail =  await _context.PaymentDetails.FirstOrDefaultAsync(m => m.PaymentDetailId == id);
            if (paymentdetail == null)
            {
                return NotFound();
            }
            PaymentDetail = paymentdetail;
           ViewData["PaymentId"] = new SelectList(_context.Payments, "PaymentId", "PaymentId");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(PaymentDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentDetailExists(PaymentDetail.PaymentDetailId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PaymentDetailExists(int id)
        {
          return (_context.PaymentDetails?.Any(e => e.PaymentDetailId == id)).GetValueOrDefault();
        }
    }
}
