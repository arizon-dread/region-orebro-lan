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
            context.Database.Migrate();
            context.Database.EnsureCreated();
            SeedSetting(context);
            context.Dispose();

        }
        private void SeedSetting(RolEfContext context) 
        {
            var item = context.ApplicationStatus.FirstOrDefault(d => d.Key == "LastSync");
            bool doSave = false;
            if (item == null)
            {
                context.ApplicationStatus.Add(new Models.ApplicationStatus { Key = "LastSync", Value = DateTime.MinValue.ToString() });
                doSave = true;
            }
            item = context.ApplicationStatus.FirstOrDefault(d => d.Key == "ServerAddress");
            if (item == null)
            {
                context.ApplicationStatus.Add(new Models.ApplicationStatus { Key = "ServerAddress", Value = "" });
                doSave = true;
            }
            item = context.ApplicationStatus.FirstOrDefault(d => d.Key == "ServiceMode");
            if (item == null)
            {
                context.ApplicationStatus.Add(new Models.ApplicationStatus { Key = "ServiceMode", Value = "Client"});
                doSave = true;
            }

            if (doSave)
            {
                context.SaveChanges();
            }

        }
    }
}
