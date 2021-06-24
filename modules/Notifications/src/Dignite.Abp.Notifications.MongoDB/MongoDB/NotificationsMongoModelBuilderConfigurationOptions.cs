using JetBrains.Annotations;
using Volo.Abp.MongoDB;

namespace Dignite.Abp.Notifications.MongoDB
{
    public class NotificationsMongoModelBuilderConfigurationOptions : AbpMongoModelBuilderConfigurationOptions
    {
        public NotificationsMongoModelBuilderConfigurationOptions(
            [NotNull] string collectionPrefix = "")
            : base(collectionPrefix)
        {
        }
    }
}