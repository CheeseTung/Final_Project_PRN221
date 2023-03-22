using System;
using System.Collections.Generic;

namespace Final_Project_PRN221.Models
{
    public partial class Room
    {
        public Room()
        {
            Electricities = new HashSet<Electricity>();
            Payments = new HashSet<Payment>();
            Users = new HashSet<User>();
        }

        public int RoomId { get; set; }
        public string RoomName { get; set; } = null!;

        public virtual ICollection<Electricity> Electricities { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
