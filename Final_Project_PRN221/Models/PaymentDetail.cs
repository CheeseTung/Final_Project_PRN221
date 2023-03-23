using System;
using System.Collections.Generic;

namespace Final_Project_PRN221.Models
{
    public partial class PaymentDetail
    {
        public PaymentDetail()
        {
            Electricities = new HashSet<Electricity>();
        }

        public int PaymentDetailId { get; set; }
        public int PaymentId { get; set; }
        public decimal RoomCharge { get; set; }
        public decimal WaterMoney { get; set; }
        public decimal NetworkMoney { get; set; }
        public decimal CleanMoney { get; set; }
        public decimal DrinkWaterMoney { get; set; }
        public decimal? Discount { get; set; }

        public virtual Payment Payment { get; set; } = null!;
        public virtual ICollection<Electricity> Electricities { get; set; }
    }
}
