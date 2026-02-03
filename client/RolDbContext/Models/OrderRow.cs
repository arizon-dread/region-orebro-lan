using System;
using System.Collections.Generic;
using System.Text;

namespace RolDbContext.Models
{
    public class OrderRow
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ItemId { get; set; }
        public int Ammount { get; set; }
       
    }
}
