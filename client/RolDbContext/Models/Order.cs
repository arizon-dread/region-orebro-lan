using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RolDbContext.Models
{
    public class Order : EntryBase
    {
        public Guid CustomerId { get; set; }
        public string DeliveryAddress { get; set; }
        public string DeliveryCity { get; set; }
        public string DeliveryPostalCode { get; set; }
        //[NotMapped]
        //public List<OrderRow> OrderRows { get; set; } = new List<OrderRow>();
    }
}
