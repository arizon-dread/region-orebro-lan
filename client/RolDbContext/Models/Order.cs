using System;
using System.Collections.Generic;
using System.Text;

namespace RolDbContext.Models
{
    public class Order : EntryBase
    {
        public Guid CustomerId { get; set; }
        public string DeliveryAddress { get; set; }
        public string DeliveryCity { get; set; }
        public string DeliveryPostalCode { get; set; }
    }
}
