using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace RolDbContext
{
    public class RolEfContext : DbContext
    {
        public RolEfContext(DbContextOptions<RolEfContext> options) : base(options) { }
        DbSet<Info> Info { get; set; }
    }
}
