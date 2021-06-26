using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dignite.Abp.Notifications.MongoDB;
using JetBrains.Annotations;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;

namespace Dignite.Abp.Notifications
{
    public class MongoNotificationSubscriptionRepository : MongoDbRepository<INotificationsMongoDbContext, NotificationSubscription>, INotificationSubscriptionRepository
    {
        public MongoNotificationSubscriptionRepository(IMongoDbContextProvider<INotificationsMongoDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<NotificationSubscription> FindAsync(Guid userId, string notificationName, [CanBeNull] string entityTypeName, [CanBeNull] string entityId, CancellationToken cancellationToken = default)
        {
            var query = await GetMongoQueryableAsync(cancellationToken);
            if (!string.IsNullOrWhiteSpace(entityTypeName))
            {
                query = query.Where(un => un.EntityTypeName == entityTypeName && un.EntityId == entityId);
            }

            return await query
                .FirstOrDefaultAsync(un =>
                    un.UserId == userId && un.NotificationName == notificationName,
                    GetCancellationToken(cancellationToken)
                );
        }

        public async Task<List<NotificationSubscription>> GetListAsync(string notificationName, [CanBeNull] string entityTypeName, [CanBeNull] string entityId, CancellationToken cancellationToken = default)
        {
            var query = await GetMongoQueryableAsync(cancellationToken);
            if (!string.IsNullOrWhiteSpace(entityTypeName))
            {
                query = query.Where(un => un.EntityTypeName == entityTypeName && un.EntityId == entityId);
            }

            return await query.Where(un => un.NotificationName == notificationName)
                        .ToListAsync(
                            GetCancellationToken(cancellationToken)
                        );
        }

        public async Task<List<NotificationSubscription>> GetListAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var query = await GetMongoQueryableAsync(cancellationToken);

            return await query.Where(un => un.UserId == userId)
                        .ToListAsync(
                            GetCancellationToken(cancellationToken)
                        );
        }
    }
}
