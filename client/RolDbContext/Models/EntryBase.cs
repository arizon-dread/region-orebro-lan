namespace RolDbContext.Models
{
    public class EntryBase
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public string? Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ItemType { get; set; }
    }
}
