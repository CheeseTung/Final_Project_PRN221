using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Final_Project_PRN221.Models;

namespace Final_Project_PRN221.Pages.Electric_Check
{
    public class DetailsModel : PageModel
    {
        private readonly Final_Project_PRN221.Models.Project_PRN221Context _context;

        public DetailsModel(Final_Project_PRN221.Models.Project_PRN221Context context)
        {
            _context = context;
        }

      public Electricity Electricity { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Electricities == null)
            {
                return NotFound();
            }

            var electricity = await _context.Electricities.FirstOrDefaultAsync(m => m.ElectricityId == id);
            if (electricity == null)
            {
                return NotFound();
            }
            else 
            {
                Electricity = electricity;
            }
            return Page();
        }
    }
}
