using Final_Project_PRN221.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Final_Project_PRN221.Pages.Admin
{
    public class SetNotificationModel : PageModel
    {
        private readonly Project_PRN221Context _context;
        public SetNotificationModel(Project_PRN221Context context)
        {
            _context = context;
        }
        [BindProperty]
        public Notification Notification { get; set; }
        public string Message { get; set; }
        public void OnGet(int? notificationID)
        {
            var noti = _context.Notifications.ToList();
            if(noti.Count() <= 0 )
            {
                Message = "There are no announcements yet";
            }
            else
            {
                if(notificationID != null)
                {
                    var notiDelete = (from n in noti
                                      where n.NotificationId== notificationID
                                      select n).SingleOrDefault();
                    _context.Notifications.Remove(notiDelete);
                    _context.SaveChanges();
                    noti.Remove(notiDelete);
                }
                ViewData["Notification"] = noti;
                Message = "Notification List";
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Notifications == null || Notification == null)
            {
                return Page();
            }

            _context.Notifications.Add(Notification);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}
