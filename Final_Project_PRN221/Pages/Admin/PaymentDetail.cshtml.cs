using Final_Project_PRN221.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Final_Project_PRN221.Pages.Admin
{
    public class PaymentDetailModel : PageModel
    {
        private readonly Project_PRN221Context _context;
        public PaymentDetailModel(Project_PRN221Context context)
        {
            _context = context;
        }

        [BindProperty]
        public int PaymentId { get; set; }
        public string RoomName { get; set; }    
        public PaymentDetail PaymentDetail { get; set; } = default!;
        public IActionResult OnGet(int? paymentId, string? roomName)
        {
            RoomName = roomName;
            if (paymentId == null || _context.PaymentDetails == null)
            {
                return NotFound();
            }

            var paymentdetail = _context.PaymentDetails.Where(pd => pd.PaymentId == paymentId).SingleOrDefault();
            if (paymentdetail == null)
            {
                return NotFound();
            }
            else
            {
                PaymentDetail = paymentdetail;
            }
            return Page();
        }
    }
}
