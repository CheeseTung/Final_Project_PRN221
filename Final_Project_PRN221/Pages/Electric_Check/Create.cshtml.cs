using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Final_Project_PRN221.Models;

namespace Final_Project_PRN221.Pages.Electric_Check
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
        ViewData["PaymentDetailId"] = new SelectList(_context.PaymentDetails, "PaymentDetailId", "PaymentDetailId");
        ViewData["RoomId"] = new SelectList(_context.Rooms, "RoomId", "RoomId");
            return Page();
        }

        [BindProperty]
        public Electricity Electricity { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Electricities == null || Electricity == null)
            {
                return Page();
            }

            _context.Electricities.Add(Electricity);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
