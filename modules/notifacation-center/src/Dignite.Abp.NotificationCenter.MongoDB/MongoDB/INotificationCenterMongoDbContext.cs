using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Dignite.Abp.NotificationCenter.MongoDB
{
    [ConnectionStringName(NotificationCenterDbProperties.ConnectionStringName)]
    public interface INotificationCenterMongoDbContext : IAbpMongoDbContext
    {
        /* Define mongo collections here. Example:
         * IMongoCollection<Question> Questions { get; }
         */
    }
}
