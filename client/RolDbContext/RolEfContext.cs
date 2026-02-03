using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RolDbContext.Models;

namespace RolDbContext
{
    public class RolEfContext : DbContext
    {
        public RolEfContext(DbContextOptions<RolEfContext> options) : base(options) { }
        DbSet<Info> Info { get; set; }
        DbSet<Order> Order { get; set; }
        DbSet<OrderRow> OrderRow { get; set; }
    }
}
