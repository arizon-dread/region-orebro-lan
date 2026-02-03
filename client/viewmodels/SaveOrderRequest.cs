using System;
using System.Collections.Generic;
using System.Text;

namespace viewmodels
{
    public class SaveOrderRequest
    {
        public Guid? Id { get; set; }
        public int Version { get; set; }
        public string? Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string DeliveryAddress { get; set; }
        public string DeliveryCity { get; set; }
        public string DeliveryPostalCode { get; set; }
        public Guid CustomerId { get; set; }
        public List<OrderRow> OrderRows { get; set; }
    }
}
