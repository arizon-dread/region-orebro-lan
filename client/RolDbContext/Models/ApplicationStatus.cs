using System.ComponentModel.DataAnnotations;

namespace RolDbContext.Models
{
    public class ApplicationStatus
    {
        [Key]
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
