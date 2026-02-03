using System;
using System.Collections.Generic;
using System.Text;

namespace viewmodels
{
    public class SyncViewModel
    {
        public DateTime RequestDate {  get; set; }
        public List<Info> Info { get; set;  }
        public List<Order> Order { get; set; }

    }
}
