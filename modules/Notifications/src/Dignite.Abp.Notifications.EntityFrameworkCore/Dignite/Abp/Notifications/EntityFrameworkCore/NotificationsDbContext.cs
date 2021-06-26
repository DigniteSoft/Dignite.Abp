using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Dignite.Abp.Notifications.EntityFrameworkCore
{
    [ConnectionStringName(NotificationsDbProperties.ConnectionStringName)]
    public class NotificationsDbContext : AbpDbContext<NotificationsDbContext>, INotificationsDbContext
    {
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }
        public DbSet<NotificationSubscription> NotificationSubscriptions { get; set; }

        public NotificationsDbContext(DbContextOptions<NotificationsDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureNotifications();
        }
    }
}