using Final_Project_PRN221.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Final_Project_PRN221.Pages
{

    public class LoginModel : PageModel
    {
        private readonly Project_PRN221Context _context;
        private readonly IConfiguration _configuration;
        public LoginModel(Project_PRN221Context context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [BindProperty]
        [Required]
        public string Username { get; set; }

        [BindProperty]
        [Required]
        public string Password { get; set; }

        public string mess { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            var user = _context.Users.SingleOrDefault(u => u.Username == Username && u.Password == Password);
            if (user == null)
            {
                mess = "Tài khoản không tồn tại hoặc tài khoản, mật khẩu đã sai!";
                return Page();
            }
            else
            {
                if (user.Role == 1)
                {
                    return RedirectToPage("/Admin/HomePage");
                }
                else
                {
                    return RedirectToPage("/Users/HomePage");
                }

            }
        }
    }
}
