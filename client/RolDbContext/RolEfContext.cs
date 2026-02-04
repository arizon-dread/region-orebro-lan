using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RolDbContext.Models;
using System.Diagnostics.Metrics;

namespace RolDbContext
{
    public class RolEfContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder
        .UseSeeding((context, _) =>
        {
            var guid = new Guid("16e0fc5c-8c50-4a2d-8893-7112a5262927");
            var item = context.Set<Item>().FirstOrDefault(x => x.Id == guid);
            if (item == null)
            {
                context.Set<Item>().Add(
                     new Item
                     {
                         Id = guid,
                         CreatedDate = DateTime.UtcNow,
                         Manufacturer = "Berras AB",
                         Name = "CD-rom skiva",
                         Price = 6.99,
                         Status = "SavedLocal",
                         UpdatedDate = DateTime.UtcNow,
                         Version = 1
                     }
                    );
                context.SaveChanges();
            }
            guid = new Guid("5b6ab900-bed9-4caa-bc02-daa8e1dbdd4f");
            item = context.Set<Item>().FirstOrDefault(x => x.Id == guid);
            if (item == null)
            {
                context.Set<Item>().Add(
                     new Item
                     {
                         Id = guid,
                         CreatedDate = DateTime.UtcNow,
                         Manufacturer = "Berras AB",
                         Name = "DVD-spelare",
                         Price = 1399,
                         Status = "SavedLocal",
                         UpdatedDate = DateTime.UtcNow,
                         Version = 1
                     }
                    );
                context.SaveChanges();
            }
            guid = new Guid("f5ec4386-6e19-4384-a1ea-8abe3b85fe71");
            var customer = context.Set<Customer>().FirstOrDefault(x => x.Id == guid);
            if (customer == null)
            {
                context.Set<Customer>().Add(
                     new Customer
                     {
                         Id = guid,
                         CreatedDate = DateTime.UtcNow,
                         Name = "Buan Jörk",
                         Status = "SavedRemote",
                         UpdatedDate = DateTime.UtcNow,
                         Version = 1,
                          DeliveryAddress = "Gatan 1",
                           DeliveryCity = "Örebro",
                            DeliveryPostalCode = "702 37",
                             Active = true,
                     }
                    );
                context.SaveChanges();
            }
        });
        
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Item>(b =>
        //    {
        //        b.HasData(
        //            new Item
        //            {
        //                Id = Guid.NewGuid(),
        //                CreatedDate = DateTime.UtcNow,
        //                Manufacturer = "Berras AB",
        //                Name = "CD-rom skiva",
        //                Price = 6.99,
        //                Status = "SavedLocal",
        //                UpdatedDate = DateTime.UtcNow,
        //                Version = 1
        //            });
        //    });
        //    base.OnModelCreating(modelBuilder);
        //}
        public RolEfContext(DbContextOptions<RolEfContext> options) : base(options) { }
        public DbSet<Info> Info { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderRow> OrderRow { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<ItemInventory> ItemInventory { get; set; }
        public DbSet<Item> Item {  get; set; }
        public DbSet<ApplicationStatus> ApplicationStatus { get; set;  }
    }
}
