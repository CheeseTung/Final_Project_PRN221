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

        [BindProperty]
        public int RoomID { get; set; }
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
                if(!p.Amount.HasValue)
                {
                    checkAmount = true;
                    break;
                }
                else
                {
                    checkAmount= false;
                }
            }
            fromDate = (DateTime)payments[0].FromDate;
            toDate = (DateTime)payments[0].ToDate;
            ViewData["payments"] = payments;
        }

        public IActionResult OnPost()
        {
            var paymentDetailList = _context.PaymentDetails.SingleOrDefault(pd => pd.PaymentId == PaymentDetail.PaymentId);
            if (paymentDetailList == null)
            {
                PaymentDetail.RoomCharge = 8000000m;
                PaymentDetail.WaterMoney = 400000m;
                PaymentDetail.NetworkMoney = 400000m;
                PaymentDetail.CleanMoney = 120000m;
                PaymentDetail.DrinkWaterMoney = 80000m;

                _context.PaymentDetails.Add(PaymentDetail);
                _context.SaveChanges();
                return RedirectToPage("/Admin/PaymentDetail", new { paymentId = PaymentDetail.PaymentId , roomID = RoomID });
            }
            else
            {
                return RedirectToPage("/Admin/PaymentDetail", new { paymentId = PaymentDetail.PaymentId , roomID = RoomID }); //Check room xem lấy được không
            }
        }

        public async Task<IActionResult> OnPostSubmitData()
        {
            fromDate = DateTime.Parse(Request.Form["fromDate"]);
            toDate = DateTime.Parse(Request.Form["toDate"]);
            //1.Update isPaid của tất cả các trường thành true
            var paymentsToUpdate = _context.Payments.Where(p => p.IsPaid == false).Where(p => p.FromDate == fromDate && p.ToDate == toDate).ToList();
            if(paymentsToUpdate.Count > 0)
            {
                paymentsToUpdate.ForEach(p => p.IsPaid = true);
                await _context.SaveChangesAsync();
                foreach (var p in paymentsToUpdate)
                {
                    var newPayment = new Payment
                    {
                        RoomId = p.RoomId,
                        Amount = null,
                        FromDate = p.FromDate.Value.AddDays(120),
                        ToDate = p.ToDate.Value.AddDays(120),
                        IsPaid = false
                    };
                    _context.Payments.Add(newPayment);
                }
               await _context.SaveChangesAsync();
                return RedirectToPage();
            }
            else
            {
                return NotFound();
            }
            //2. Create dữ liệu mới (Tăng ngày fromDate, toDate, isPaid = false, Amount = null -> k cần set, RoomID dữ nguyên)          
        }
    }
}
