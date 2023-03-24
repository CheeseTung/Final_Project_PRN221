using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Final_Project_PRN221.Models;

namespace Final_Project_PRN221.Pages.Electric_Check
{
    public class EditModel : PageModel
    {
        private readonly Final_Project_PRN221.Models.Project_PRN221Context _context;

        public EditModel(Final_Project_PRN221.Models.Project_PRN221Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Electricity Electricity { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Electricities == null)
            {
                return NotFound();
            }

            var electricity =  await _context.Electricities.FirstOrDefaultAsync(m => m.ElectricityId == id);
            if (electricity == null)
            {
                return NotFound();
            }
            Electricity = electricity;
           ViewData["PaymentDetailId"] = new SelectList(_context.PaymentDetails, "PaymentDetailId", "PaymentDetailId");
           ViewData["RoomId"] = new SelectList(_context.Rooms, "RoomId", "RoomId");
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

            _context.Attach(Electricity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ElectricityExists(Electricity.ElectricityId))
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

        private bool ElectricityExists(int id)
        {
          return (_context.Electricities?.Any(e => e.ElectricityId == id)).GetValueOrDefault();
        }
    }
}
