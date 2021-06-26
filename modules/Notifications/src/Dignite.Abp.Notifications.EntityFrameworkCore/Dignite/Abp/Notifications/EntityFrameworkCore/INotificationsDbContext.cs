using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Dignite.Abp.Notifications.EntityFrameworkCore
{
    [ConnectionStringName(NotificationsDbProperties.ConnectionStringName)]
    public interface INotificationsDbContext : IEfCoreDbContext
    {
        DbSet<Notification> Notifications { get; }
        DbSet<UserNotification> UserNotifications { get; }
        DbSet<NotificationSubscription> NotificationSubscriptions { get; }
    }
}