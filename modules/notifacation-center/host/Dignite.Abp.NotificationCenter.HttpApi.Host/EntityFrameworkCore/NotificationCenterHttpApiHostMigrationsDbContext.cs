using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Dignite.Abp.NotificationCenter.EntityFrameworkCore
{
    public class NotificationCenterHttpApiHostMigrationsDbContext : AbpDbContext<NotificationCenterHttpApiHostMigrationsDbContext>
    {
        public NotificationCenterHttpApiHostMigrationsDbContext(DbContextOptions<NotificationCenterHttpApiHostMigrationsDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigureNotificationCenter();
        }
    }
}
