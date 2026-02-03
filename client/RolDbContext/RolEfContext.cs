using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RolDbContext.Models;

namespace RolDbContext
{
    public class RolEfContext : DbContext
    {
        public RolEfContext(DbContextOptions<RolEfContext> options) : base(options) { }
        public DbSet<Info> Info { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderRow> OrderRow { get; set; }
    }
}
