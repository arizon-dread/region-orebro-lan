using System;
using System.Collections.Generic;
using System.Text;

namespace RolDbContext.Models
{
    public class Customer : EntryBase
    {
        public string Name { get; set; }
        public string DeliveryAddress { get; set; }
        public string DeliveryCity { get; set; }
        public string DeliveryPostalCode { get; set; }
        public bool Active { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
