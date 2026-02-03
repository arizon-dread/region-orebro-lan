using System;
using System.Collections.Generic;
using System.Text;

namespace RolDbContext.Models
{
    public class ItemInventory : EntryBase
    {
        public Guid ItemId { get; set; }
        public int Inventory { get; set; }
    }
}
