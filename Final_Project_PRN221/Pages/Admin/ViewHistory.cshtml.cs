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
        public int RoomId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public void OnGet()
        {
            DateTime maxFromDate = _context.Payments.OrderByDescending(p => p.FromDate).FirstOrDefault()?.FromDate ?? DateTime.MinValue;
            var query = _context.Payments.Include(p => p.Room);
            var payment = (from p in query
                           where p.IsPaid == true && p.ToDate < maxFromDate
                           select p).ToList();
            var room = _context.Rooms.ToList();
            ViewData["Rooms"] = room;
            RoomId = Request.Query.ContainsKey("rooms") ? int.Parse(Request.Query["rooms"]) : 0;

            if (Request.Query.ContainsKey("fromDate"))
            {
                if (string.IsNullOrEmpty(Request.Query["fromDate"]))
                {
                    FromDate = DateTime.MinValue;
                }
                else
                {
                    FromDate = DateTime.Parse(Request.Query["fromDate"]);
                }
            }
            if (Request.Query.ContainsKey("toDate"))
            {
                if (string.IsNullOrEmpty(Request.Query["toDate"]))
                {
                    ToDate = DateTime.MinValue;
                }
                else
                {
                    ToDate = DateTime.Parse(Request.Query["toDate"]);
                }
            }

            //Việc search đang lỗi
            if (RoomId != 0 && FromDate == null && ToDate == null)
            {
                payment = (from p in query
                           where p.IsPaid == true && p.ToDate < maxFromDate && p.RoomId == RoomId
                           select p).ToList();
            } else if (RoomId != 0 && FromDate != null && ToDate == null)
            {
                payment = (from p in query
                           where p.IsPaid == true && p.ToDate < maxFromDate && p.RoomId == RoomId && p.FromDate >= FromDate
                           select p).ToList();
            }
            else if (RoomId != 0 && FromDate != null && ToDate != null)
            {
                payment = (from p in query
                           where p.IsPaid == true && p.ToDate < maxFromDate && p.RoomId == RoomId && p.FromDate >= FromDate && p.ToDate <= ToDate
                           select p).ToList();
            }
            else if (RoomId != 0 && FromDate == null && ToDate != null)
            {
                payment = (from p in query
                           where p.IsPaid == true && p.ToDate < maxFromDate && p.RoomId == RoomId && p.ToDate <= ToDate
                           select p).ToList();
            }
            else if (RoomId == 0 && FromDate == null && ToDate != null)
            {
                payment = (from p in query
                           where p.IsPaid == true && p.ToDate < maxFromDate  && p.ToDate <= ToDate
                           select p).ToList();
            }
            else if (RoomId == 0 && FromDate != null && ToDate == null)
            {
                payment = (from p in query
                           where p.IsPaid == true && p.ToDate < maxFromDate && p.FromDate >= FromDate
                           select p).ToList();
            }
            else if (RoomId == 0 && FromDate != null && ToDate != null)
            {
                payment = (from p in query
                           where p.IsPaid == true && p.ToDate < maxFromDate && p.FromDate >= FromDate && p.ToDate <= ToDate
                           select p).ToList();
            }
            ViewData["Payment"] = payment;
        }
    }
}
