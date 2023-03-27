using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Final_Project_PRN221.Models;

namespace Final_Project_PRN221.Pages.Notification_Check
{
    public class DeleteModel : PageModel
    {
        private readonly Final_Project_PRN221.Models.Project_PRN221Context _context;

        public DeleteModel(Final_Project_PRN221.Models.Project_PRN221Context context)
        {
            _context = context;
        }

        [BindProperty]
      public Notification Notification { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Notifications == null)
            {
                return NotFound();
            }

            var notification = await _context.Notifications.FirstOrDefaultAsync(m => m.NotificationId == id);

            if (notification == null)
            {
                return NotFound();
            }
            else 
            {
                Notification = notification;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Notifications == null)
            {
                return NotFound();
            }
            var notification = await _context.Notifications.FindAsync(id);

            if (notification != null)
            {
                Notification = notification;
                _context.Notifications.Remove(Notification);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
