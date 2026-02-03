namespace RolDbContext.Models
{
    public class OrderRow : EntryBase
    {
        public Guid OrderId { get; set; }
        public Guid ItemId { get; set; }
        public int Ammount { get; set; }
       
    }
}
