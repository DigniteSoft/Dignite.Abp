using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Dignite.Abp.NotificationCenter.MongoDB
{
    [ConnectionStringName(NotificationCenterDbProperties.ConnectionStringName)]
    public class NotificationCenterMongoDbContext : AbpMongoDbContext, INotificationCenterMongoDbContext
    {
        /* Add mongo collections here. Example:
         * public IMongoCollection<Question> Questions => Collection<Question>();
         */

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.ConfigureNotificationCenter();
        }
    }
}