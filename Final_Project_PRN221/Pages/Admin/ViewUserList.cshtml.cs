using Final_Project_PRN221.Models;
using Final_Project_PRN221.ServiceData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Final_Project_PRN221.Pages.Admin
{
    public class ViewUserListModel : PageModel
    {
        private readonly Project_PRN221Context _context;
        private readonly DataService dataService;
        public ViewUserListModel(Project_PRN221Context context)
        {
            _context = context;
        }
        [BindProperty(SupportsGet = true)]
        public List<User> Users { get; set; } = default;
        public void OnGet(string? deleteUsername)
        {
            var query = _context.Users.Include(u => u.Room);
            var users = (from u in query
                         where u.Role == 2
                         select u).ToList();
            if (deleteUsername != null)
            {
                var userDelete = (from u in users
                                  where u.Username == deleteUsername
                                  select u).SingleOrDefault();
                _context.Users.Remove(userDelete);
                _context.SaveChanges();
                users.Remove(userDelete);
            }
                        
            ViewData["users"] = users;
        }
    }
}
