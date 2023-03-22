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
        public int? RoomId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public decimal? PricePerNumber { get; set; }
        public int? Quantity { get; set; }
        public string? Total { get; set; }

        public virtual Room? Room { get; set; }
        public virtual ICollection<PaymentDetail> PaymentDetails { get; set; }
    }
}
