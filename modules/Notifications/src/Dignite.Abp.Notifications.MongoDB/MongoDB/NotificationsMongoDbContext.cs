using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Dignite.Abp.Notifications.MongoDB
{
    [ConnectionStringName(NotificationsDbProperties.ConnectionStringName)]
    public class NotificationsMongoDbContext : AbpMongoDbContext, INotificationsMongoDbContext
    {
        /* Add mongo collections here. Example:
         * public IMongoCollection<Question> Questions => Collection<Question>();
         */

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.ConfigureNotifications();
        }
    }
}