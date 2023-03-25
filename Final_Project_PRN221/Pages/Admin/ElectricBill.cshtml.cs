using Final_Project_PRN221.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Final_Project_PRN221.Pages.Admin
{
    public class ElectricBillModel : PageModel
    {
        private readonly Project_PRN221Context _context;
        public ElectricBillModel(Project_PRN221Context context)
        {
            _context = context;
        }
        public DateTime fromDate = new DateTime();
        public DateTime toDate = new DateTime();
        public decimal total { get; set; }
        public string RoomName { get; set; }
        [BindProperty]
        public int _RoomID { get; set; }
        [BindProperty]
        public Electricity Electricity { get; set; } = default!;
        public int _PaymentId { get; set; }
        public IActionResult OnGet(int? paymentDetailId)
        {
            var _paymentDetail = _context.PaymentDetails.SingleOrDefault(pd => pd.PaymentDetailId == paymentDetailId);
            _PaymentId = _paymentDetail.PaymentId;
            if (paymentDetailId == null)
            {
                return NotFound();
            }
            var query = _context.Electricities.Include(e => e.PaymentDetail).Include(e => e.Room);
            var electricityBill = (from e in query
                                   where e.PaymentDetailId == paymentDetailId
                                   select e).SingleOrDefault();
            if (electricityBill != null)
            {
                fromDate = (DateTime)electricityBill.FromDate;
                toDate = (DateTime)electricityBill.ToDate;
                var room = _context.Rooms.SingleOrDefault(r => r.RoomId == electricityBill.RoomId);
                RoomName = room.RoomName;
                _RoomID = room.RoomId;
                Electricity = electricityBill;
                return Page();
            }
            else
            {
                return NotFound();
            }
        }

        public  IActionResult OnPost()
        {
            _PaymentId = Convert.ToInt32(Request.Form["PaymentId"]);
            Electricity.PricePerNumber = 3000m;
            Electricity.Total = Electricity.PricePerNumber * Electricity.Quantity;
            _context.Entry(Electricity).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ElectricityExists(Electricity.ElectricityId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToPage("/Admin/PaymentDetail", new { paymentId = _PaymentId, roomID = Electricity.RoomId }); //paymentId = 3, roomID = 2
            //dang bị lỗi paymentID = 0;
        }
        private bool ElectricityExists(int id)
        {
            return (_context.Electricities?.Any(e => e.ElectricityId == id)).GetValueOrDefault();
        }
    }
}
