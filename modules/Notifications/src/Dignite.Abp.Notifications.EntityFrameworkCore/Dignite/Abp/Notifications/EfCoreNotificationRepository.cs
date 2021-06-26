using Dignite.Abp.Notifications.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Dignite.Abp.Notifications
{
    public class EfCoreNotificationRepository : EfCoreRepository<INotificationsDbContext, Notification,Guid>, INotificationRepository
    {
        public EfCoreNotificationRepository(
            IDbContextProvider<INotificationsDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}
