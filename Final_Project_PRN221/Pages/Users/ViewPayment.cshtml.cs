using Final_Project_PRN221.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Final_Project_PRN221.Pages.Users
{
    public class ViewPaymentModel : PageModel
    {
        private readonly Project_PRN221Context _context;
        public ViewPaymentModel(Project_PRN221Context context)
        {
            _context = context;
        }
        public DateTime fromDate = new DateTime();
        public DateTime toDate = new DateTime();
        public bool checkAmount { get; set; }
        public async Task OnGetAsync()
        {
            var query = _context.Payments.Include(p => p.Room);
            var payments = (from p in query
                            where p.IsPaid == false
                            orderby p.Room.RoomName
                            select p).ToList();
            foreach (var p in payments)
            {
                if (!p.Amount.HasValue)
                {
                    checkAmount = true;
                    break;
                }
                else
                {
                    checkAmount = false;
                }
            }
            fromDate = (DateTime)payments[0].FromDate;
            toDate = (DateTime)payments[0].ToDate;
            ViewData["payments"] = payments;
        }
    }
}
