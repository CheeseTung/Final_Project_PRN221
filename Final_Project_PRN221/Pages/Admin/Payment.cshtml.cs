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
        public DateTime fromDate = new DateTime();
        public DateTime toDate = new DateTime();
        [BindProperty]
        public PaymentDetail PaymentDetail { get; set; }
        public void OnGet()
        {
            var query = _context.Payments.Include(p => p.Room);
            var payments = (from p in query
                            where p.IsPaid == false
                            orderby p.Room.RoomName
                            select p).ToList();
            fromDate = (DateTime)payments[0].FromDate;
            toDate = (DateTime)payments[0].ToDate;
            ViewData["payments"] = payments;
        }

        public IActionResult OnPost()
        {
            var paymentDetailList = _context.PaymentDetails.SingleOrDefault(pd => pd.PaymentId == PaymentDetail.PaymentId);
            if (paymentDetailList != null)
            {
                PaymentDetail.RoomCharge = 8000000m;
                PaymentDetail.WaterMoney = 400000m;
                PaymentDetail.NetworkMoney = 400000m;
                PaymentDetail.CleanMoney = 120000m;
                PaymentDetail.DrinkWaterMoney = 80000m;

                _context.PaymentDetails.Add(PaymentDetail);
                _context.SaveChanges();
                return RedirectToPage($"/Admin/PaymentDetail?paymentId={PaymentDetail.PaymentId}&&roomName={PaymentDetail.Payment.Room.RoomName}");
            }
            else
            {
                return RedirectToPage($"/Admin/PaymentDetail?paymentId={PaymentDetail.PaymentId}&&roomName={PaymentDetail.Payment.Room.RoomName}"); //Check room xem lấy được không
            }
        }
    }
}
