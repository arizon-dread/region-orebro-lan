using Microsoft.EntityFrameworkCore;

namespace RolDbContext
{
    public class DbHelper
    {
        public void Init(string connectionString)
        {
            var dbContextBuilder = new DbContextOptionsBuilder<RolEfContext>();
            var connString = @"Data Source=orderdatabase.db";
            dbContextBuilder.UseSqlite(connString);

            var context = new RolEfContext(dbContextBuilder.Options);
            try
            {
                context.Database.Migrate();
            }
            catch { }
            context.Database.EnsureCreated();
            context.Dispose();

        }
    }
}
