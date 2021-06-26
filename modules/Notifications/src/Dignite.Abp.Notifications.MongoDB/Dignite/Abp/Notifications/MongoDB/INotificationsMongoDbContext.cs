using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Dignite.Abp.Notifications.MongoDB
{
    [ConnectionStringName(NotificationsDbProperties.ConnectionStringName)]
    public interface INotificationsMongoDbContext : IAbpMongoDbContext
    {
        IMongoCollection<Notification> Notifications { get; }
        IMongoCollection<UserNotification> UserNotifications { get; }
        IMongoCollection<NotificationSubscription> NotificationSubscriptions { get; }
    }
}
