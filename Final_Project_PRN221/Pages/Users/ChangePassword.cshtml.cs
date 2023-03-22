using Final_Project_PRN221.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Final_Project_PRN221.Pages.Users
{
    public class ChangePasswordModel : PageModel
    {
        private readonly Project_PRN221Context _context;
        public ChangePasswordModel(Project_PRN221Context context)
        {
            _context = context;
        }
        [BindProperty]
        [Required]
        public string Username { get; set; }

        [BindProperty]
        [Required]
        public string oldPassword { get; set; }

        [BindProperty]
        [Required]
        public string newPassword { get; set; }
        [BindProperty]
        [Required]
        public string ConfirmPassword { get; set; }
        public string mess { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            var userChangePass = _context.Users.SingleOrDefault(x => x.Username == Username);

            if (userChangePass != null)
            {
                if (!oldPassword.Equals(userChangePass.Password))
                {
                    mess = "Mật khẩu cũ không đúng"!;
                    return Page();
                }
                else
                {
                    if (oldPassword.Equals(newPassword))
                    {
                        mess = "Mật khẩu mới trùng với mật khẩu cũ! Mời nhập lại";
                        return Page();
                    }
                    else
                    {
                        if (!newPassword.Equals(ConfirmPassword))
                        {
                            mess = "Việc nhập lại mật khẩu không trùng khớp với mật khẩu mới! Thử lại";
                            return Page();
                        }
                        else
                        {
                            userChangePass.Password = ConfirmPassword;
                            _context.Attach(userChangePass).State = EntityState.Modified;
                            _context.SaveChanges();
                            mess = "Đổi mật khẩu thành công";
                            return Page();
                        }
                    }
                }
            }
            else
            {
                mess = "Tên tài khoản không tồn tại!";
                return Page();
            }
        }
    }
}
