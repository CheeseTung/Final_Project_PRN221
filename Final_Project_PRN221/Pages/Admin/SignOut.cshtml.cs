using Final_Project_PRN221.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Final_Project_PRN221.Pages.Admin
{

    public class SignOutModel : PageModel
    {
        private readonly Project_PRN221Context _context;
        public SignOutModel(Project_PRN221Context context)
        {
            _context = context;
        }
        [BindProperty] public User user { get; set; } = default;

        public string Mess { get; set; } = default;

        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {

            var userCheck = _context.Users.SingleOrDefault(u => u.Username == user.Username);
            if (userCheck == null)
            {
                user.Role = 2;
                _context.Users.Add(user);
                _context.SaveChanges();
                Mess = "Tạo tài khoản thành công";
                return Page();
            }
            else
            {
                Mess = "Tài khoản đã tồn tại!";
                return Page();
            }
        }
    }
}
