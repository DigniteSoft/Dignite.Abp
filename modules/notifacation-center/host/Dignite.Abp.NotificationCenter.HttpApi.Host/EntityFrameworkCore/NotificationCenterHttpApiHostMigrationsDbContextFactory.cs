using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Dignite.Abp.NotificationCenter.EntityFrameworkCore
{
    public class NotificationCenterHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<NotificationCenterHttpApiHostMigrationsDbContext>
    {
        public NotificationCenterHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<NotificationCenterHttpApiHostMigrationsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("NotificationCenter"));

            return new NotificationCenterHttpApiHostMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
