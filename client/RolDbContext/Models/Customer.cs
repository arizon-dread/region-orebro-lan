using System;
using System.Collections.Generic;
using System.Text;

namespace RolDbContext.Models
{
    public class Customer : EntryBase
    {
        public string DeliveryAddress { get; set; }
        public string DeliveryCity { get; set; }
        public string DeliveryPostalCode { get; set; }
    }
}
