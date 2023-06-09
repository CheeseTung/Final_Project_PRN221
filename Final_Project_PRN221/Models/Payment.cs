﻿using System;
using System.Collections.Generic;

namespace Final_Project_PRN221.Models
{
    public partial class Payment
    {
        public Payment()
        {
            PaymentDetails = new HashSet<PaymentDetail>();
        }

        public int PaymentId { get; set; }
        public int? RoomId { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public bool? IsPaid { get; set; }

        public virtual Room? Room { get; set; }
        public virtual ICollection<PaymentDetail> PaymentDetails { get; set; }
    }
}
