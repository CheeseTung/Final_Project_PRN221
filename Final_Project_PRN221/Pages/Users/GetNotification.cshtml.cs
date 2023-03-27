using Final_Project_PRN221.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Final_Project_PRN221.Pages.Users
{
    public class GetNotificationModel : PageModel
    {
        private readonly Project_PRN221Context _context;
        public GetNotificationModel(Project_PRN221Context context)
        {
            _context = context;
        }
        [BindProperty]
        public Notification Notification { get; set; }
        public string Message { get; set; }
        public void OnGet()
        {
            var noti = _context.Notifications.ToList();
            if (noti.Count() <= 0)
            {
                Message = "Chưa có thông báo nào";
            }
            else
            {
                ViewData["Notification"] = noti;
                Message = "Danh sách thông báo";
            }
        }
    }
}
