using Volo.Abp;
using Volo.Abp.MongoDB;

namespace Dignite.Abp.NotificationCenter.MongoDB
{
    public static class NotificationCenterMongoDbContextExtensions
    {
        public static void ConfigureNotificationCenter(
            this IMongoModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));
        }
    }
}
