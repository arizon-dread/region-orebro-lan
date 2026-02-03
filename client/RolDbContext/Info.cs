namespace RolDbContext
{
    public class Info : EntryBase
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime? Unpublished{ get; set; }
    }
}
