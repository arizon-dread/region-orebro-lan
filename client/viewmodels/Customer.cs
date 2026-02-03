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
    }
}
