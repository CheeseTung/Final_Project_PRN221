using Final_Project_PRN221.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Final_Project_PRN221.Pages.Admin
{
    public class ViewHistoryModel : PageModel
    {
        private readonly Project_PRN221Context _context;
        public ViewHistoryModel(Project_PRN221Context context)
        {
            _context = context;
        }
        public void OnGet()
        {
            var query = _context.Payments.Include(p => p.Room);
            var payment = (from p in query
                           where p.IsPaid == true
                           select p).ToList();
            ViewData["Payment"] = payment;
        }
    }
}
