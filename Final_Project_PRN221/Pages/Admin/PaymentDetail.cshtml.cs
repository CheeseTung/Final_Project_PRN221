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
        [BindProperty]
        public int PaymentDetailId { get; set; }
        public string RoomName { get; set; }
        public int RoomID { get; set; }
        [BindProperty]
        public PaymentDetail PaymentDetail { get; set; } = default!;

        public decimal? TotalElectricAmount { get; set; }
        public IActionResult OnGet(int? paymentId, int? roomID)
        {
            if (paymentId == null || _context.PaymentDetails == null || roomID == null)
            {
                return NotFound();
            }
            var room = _context.Rooms.SingleOrDefault(r => r.RoomId == roomID);
            RoomName = room.RoomName;
            RoomID = room.RoomId;
            var paymentdetail = _context.PaymentDetails.Where(pd => pd.PaymentId == paymentId).SingleOrDefault();
            PaymentId = paymentdetail.PaymentId;
            PaymentDetailId = paymentdetail.PaymentDetailId;
            //Check electric total
            var query = _context.Electricities.Include(e => e.PaymentDetail);
            var electricResult = (from e in query
                                  where e.PaymentDetailId == paymentdetail.PaymentDetailId
                                  select e).SingleOrDefault();
            if (electricResult == null)
            {
                TotalElectricAmount = null;
            }
            else
            {
                TotalElectricAmount = electricResult.Total;
            }
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

        [BindProperty]
        public Electricity Electricity { get; set; } = default!;
        private decimal DiscountValue = 0;
        private bool checkExist;
        public IActionResult OnPost()
        {
            TotalElectricAmount = decimal.TryParse(Request.Form["TotalElectricAmount"].ToString(), out decimal result) ? result : 0;

            if (TotalElectricAmount == null || TotalElectricAmount == 0)
            {
                var query = _context.Payments.Include(p => p.PaymentDetails);
                var getDateFromPayment = (from fd in query
                                          where fd.PaymentId == PaymentId
                                          select fd).SingleOrDefault();
                //Check null
                var checkExitsList = _context.Electricities.ToList();
                foreach (var item in checkExitsList)
                {
                    if(item.PaymentDetailId == PaymentDetailId)
                    {
                        checkExist = true;
                    }
                    else
                    {
                        checkExist = false;
                    }
                }
                if (!checkExist)
                {
                    return RedirectToPage("/Admin/ElectricBill", new { paymentDetailId = Electricity.PaymentDetailId });
                }
                else
                {
                    Electricity.FromDate = getDateFromPayment.FromDate;
                    Electricity.ToDate = getDateFromPayment.ToDate;
                    Electricity.PricePerNumber = 3000m;

                    _context.Electricities.Add(Electricity);
                    _context.SaveChanges();
                    return RedirectToPage("/Admin/ElectricBill", new { paymentDetailId = Electricity.PaymentDetailId });
                }
            }
            else
            {               
                //if (!ModelState.IsValid)
                //{
                //    return Page();
                //}

                _context.Attach(PaymentDetail).State = EntityState.Modified;

                try
                {
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentDetailExists(PaymentDetail.PaymentDetailId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                var paymentUpdateAmount = _context.Payments.SingleOrDefault(p => p.PaymentId == PaymentDetail.PaymentId);
                if (paymentUpdateAmount != null)
                {
                    if(PaymentDetail.Discount == null)
                    {
                        DiscountValue = 0;
                    }
                    else
                    {
                        DiscountValue = (decimal)PaymentDetail.Discount;
                    }
                    paymentUpdateAmount.Amount = PaymentDetail.RoomCharge + PaymentDetail.WaterMoney + PaymentDetail.NetworkMoney + PaymentDetail.DrinkWaterMoney + PaymentDetail.CleanMoney + TotalElectricAmount - DiscountValue;
                    _context.Attach(paymentUpdateAmount).State = EntityState.Modified;
                    _context.SaveChanges();
                    return RedirectToPage("/Admin/Payment");
                }
                else
                {
                    return NotFound();
                }

            }
        }
        private bool PaymentDetailExists(int id)
        {
            return (_context.PaymentDetails?.Any(e => e.PaymentDetailId == id)).GetValueOrDefault();
        }
    }
}
