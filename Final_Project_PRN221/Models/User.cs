using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Final_Project_PRN221.Models
{
    public partial class User
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Fullname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int Phone { get; set; }
        public int Role { get; set; }
        [Required]
        [Range(1,2,ErrorMessage = "Bạn chỉ có thể nhập RoomID từ 1 đến 2")]
        public int RoomId { get; set; }

        public virtual Room Room { get; set; } = null!;
    }
}
