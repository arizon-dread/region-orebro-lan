using System;
using System.Collections.Generic;
using System.Text;

namespace viewmodels
{
    public class Customer
    {
        public Guid? Id { get; set; }
        public int Version { get; set; }
        public string? Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Name { get; set; }
        public string? DeliveryAddress { get; set; }
        public string? DeliveryCity { get; set; }
        public string? DeliveryPostalCode { get; set; }
        public bool Active { get; set; }
        public List<Order>? Orders { get; set; } = new List<Order>();

    }
}
