using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Config.ConfigModels;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Config
{
    public static class AppConfig
    {
        public static ConnectionStringConfig ConnectionStringConfig { get; set; }
        public static JwtConfiguration JwtConfiguration { get; set; }




        public static void MigrationsInit()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var builder = new ConfigurationBuilder()
                .SetBasePath($"{Directory.GetCurrentDirectory()}/../Api/")
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environment}.json");

            var Configuration = builder.Build();
            Init(Configuration);
        }

        public static void Init(IConfiguration Configuration)
        {
            var connectionStringConfig = Configuration.GetSection("ConnectionStringsSettings");
            ConnectionStringConfig = connectionStringConfig.Get<ConnectionStringConfig>();

            var jwtConfig = Configuration.GetSection("JwtConfiguration");
            JwtConfiguration = jwtConfig.Get<JwtConfiguration>();

           
        }
    }
}
