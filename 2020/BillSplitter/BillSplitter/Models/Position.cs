﻿using System.Collections.Generic;

namespace BillSplitter.Models
{
    public class Position
    {
        public int Id { get; set; }

        public int BillId { get; set; }
        public virtual Bill Bill { get; set; }

        public decimal Price { get; set; }
        public string Name { get; set; }
        public virtual List<Order> Orders { get; set; }
        public int Quantity { get; set; }
        public int ManagingMemberId { get; set; }
        public virtual Member ManagingMember { get; set; }
    }
}
