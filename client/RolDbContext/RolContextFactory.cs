using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace RolDbContext
{
    internal class RolContextFactory : IDesignTimeDbContextFactory<RolEfContext>
    {
        public RolEfContext CreateDbContext(string[] args)
        {
            var dbContextBuilder = new DbContextOptionsBuilder<RolEfContext>();
            var connString = @"Data Source=C:\\temp\\db\\testdb.db";
            dbContextBuilder.UseSqlite(connString);
            return new RolEfContext(dbContextBuilder.Options);
        }
    }
}
