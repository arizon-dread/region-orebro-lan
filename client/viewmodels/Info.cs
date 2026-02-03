using System;

namespace viewmodels
{
    public class Info
    {
        public Guid? Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime? PublishDate { get; set; }
        public DateTime? Unpublished { get; set; }
        public int Version { get; set; }
        public string? Status { get; set; }
    }
}
