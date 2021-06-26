using Dignite.Abp.Notifications.MongoDB;
using System;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;

namespace Dignite.Abp.Notifications
{
    public class MongoNotificationRepository : MongoDbRepository<INotificationsMongoDbContext, Notification, Guid>, INotificationRepository
    {
        public MongoNotificationRepository(IMongoDbContextProvider<INotificationsMongoDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
