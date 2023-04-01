using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Design;
using Infrastructure.Config;

namespace Core.Database.Context
{
    public class DbContextMigrationsFactory : IDesignTimeDbContextFactory<HackatonDbContext>
    {
        public HackatonDbContext CreateDbContext(string[] args)
        {
            AppConfig.MigrationsInit();
            return new HackatonDbContext();
        }
    }
    
    
}
