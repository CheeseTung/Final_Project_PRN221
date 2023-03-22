using Final_Project_PRN221.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Final_Project_PRN221.Pages.Admin
{
    public class PaymentModel : PageModel
    {
        private readonly Project_PRN221Context _context;
        public PaymentModel(Project_PRN221Context context)
        {
            _context = context;
        }
        List<Room> Rooms = new List<Room>();
        List<Payment> Payments = new List<Payment>();
        public DateTime fromDate = new DateTime();
        public DateTime toDate = new DateTime();
        public void OnGet()
        {
            var query = _context.Rooms.Include(r => r.Payments);

        }
    }
}
