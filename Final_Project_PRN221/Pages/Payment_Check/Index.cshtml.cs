using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Final_Project_PRN221.Models;

namespace Final_Project_PRN221.Pages.Payment_Check
{
    public class IndexModel : PageModel
    {
        private readonly Final_Project_PRN221.Models.Project_PRN221Context _context;

        public IndexModel(Final_Project_PRN221.Models.Project_PRN221Context context)
        {
            _context = context;
        }

        public IList<Payment> Payment { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Payments != null)
            {
                Payment = await _context.Payments
                .Include(p => p.Room).ToListAsync();
            }
        }
    }
}
