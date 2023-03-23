using System;
using System.Collections.Generic;

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
        public int RoomId { get; set; }

        public virtual Room Room { get; set; } = null!;
    }
}
