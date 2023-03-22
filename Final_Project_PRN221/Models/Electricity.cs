using System;
using System.Collections.Generic;

namespace Final_Project_PRN221.Models
{
    public partial class Electricity
    {
        public Electricity()
        {
            PaymentDetails = new HashSet<PaymentDetail>();
        }

        public int ElectricityId { get; set; }
        public int RoomId { get; set; }
        public DateTime Month { get; set; }
        public int Usage { get; set; }

        public virtual Room Room { get; set; } = null!;
        public virtual ICollection<PaymentDetail> PaymentDetails { get; set; }
    }
}
