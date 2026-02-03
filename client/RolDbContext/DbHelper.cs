using Microsoft.EntityFrameworkCore;

namespace RolDbContext
{
    public class DbHelper
    {
        public void Init(string connectionString)
        {
            var dbContextBuilder = new DbContextOptionsBuilder<RolEfContext>();
            dbContextBuilder.UseSqlite(connectionString);

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
