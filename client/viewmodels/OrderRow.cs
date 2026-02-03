using System;
using System.Collections.Generic;

namespace viewmodels
{
    public class OrderRow
    {
        public Guid? Id { get; set; }
        public int Version { get; set; }
        public string? Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Item Item { get; set; }
        public int Ammount { get; set; }
    }
}
