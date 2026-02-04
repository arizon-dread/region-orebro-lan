using System;
using System.Collections.Generic;
using System.Text;

namespace RolDbContext.Models
{
    public class Item : EntryBase
    {
        public string Manufacturer { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

    }
}
