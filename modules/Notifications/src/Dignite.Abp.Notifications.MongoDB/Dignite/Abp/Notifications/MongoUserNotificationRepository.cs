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
    public class MongoUserNotificationRepository : MongoDbRepository<INotificationsMongoDbContext, UserNotification>, IUserNotificationRepository
    {
        public MongoUserNotificationRepository(IMongoDbContextProvider<INotificationsMongoDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<UserNotification> FindAsync(Guid userId, Guid notificationId, CancellationToken cancellationToken = default)
        {
            var query = await GetMongoQueryableAsync(cancellationToken);

            return await query
                       .FirstOrDefaultAsync(un => 
                           un.UserId==userId && un.NotificationId==notificationId, 
                           GetCancellationToken(cancellationToken)
                       );
        }

        public async Task<List<UserNotification>> GetListAsync(Guid userId, UserNotificationState? state = null, int skipCount = 0, int maxResultCount = int.MaxValue, DateTime? startDate = null, DateTime? endDate = null, CancellationToken cancellationToken = default)
        {
            var result = await (await GetMongoQueryableAsync(cancellationToken))
                .WhereIf(state != null, un => un.State == state)
                .WhereIf(startDate != null, un => un.Notification.CreationTime >= startDate.Value)
                .WhereIf(endDate != null, un => un.Notification.CreationTime <= endDate.Value)
                .OrderByDescending(un=>un.Notification.CreationTime)
                .Skip(skipCount)
                .Take(maxResultCount)
                .As<IMongoQueryable<UserNotification>>()
                .ToListAsync(GetCancellationToken(cancellationToken));
            var notificationIds = result.Select(un => un.NotificationId).ToArray();
            var notifications = await (await GetDbContextAsync(cancellationToken)).Notifications.AsQueryable()
                .Where(n => notificationIds.Any(nid=>nid==n.Id))
                .ToListAsync(cancellationToken);

            foreach (var un in result)
            {
                un.Notification = notifications.Single(n => n.Id == un.NotificationId);
            }

            return result;
        }

        public async Task<int> GetCountAsync(Guid userId, UserNotificationState? state = null, DateTime? startDate = null, DateTime? endDate = null, CancellationToken cancellationToken = default)
        {
            return await(await GetMongoQueryableAsync(cancellationToken))
                .WhereIf(state != null, un => un.State == state)
                .WhereIf(startDate != null, un => un.Notification.CreationTime >= startDate.Value)
                .WhereIf(endDate != null, un => un.Notification.CreationTime <= endDate.Value)
                .As<IMongoQueryable<UserNotification>>()
                .Where(un => un.UserId == userId)
                .CountAsync(GetCancellationToken(cancellationToken));
        }


        public async Task<bool> AnyAsync(Guid notificationId, Guid ignoredUserId, CancellationToken cancellationToken = default)
        {
            var query = await GetMongoQueryableAsync(cancellationToken);

            return await query
                       .AnyAsync(un => un.NotificationId == notificationId && un.UserId != ignoredUserId, GetCancellationToken(cancellationToken));
        }
    }
}
