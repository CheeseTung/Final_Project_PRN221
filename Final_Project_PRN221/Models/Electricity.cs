using System;
using System.Collections.Generic;

namespace Final_Project_PRN221.Models
{
    public partial class Electricity
    {
        public int ElectricityId { get; set; }
        public int? RoomId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public decimal? PricePerNumber { get; set; }
        public int? Quantity { get; set; }
        public decimal? Total { get; set; }
        public int? PaymentDetailId { get; set; }

        public virtual PaymentDetail? PaymentDetail { get; set; }
        public virtual Room? Room { get; set; }
    }
}
