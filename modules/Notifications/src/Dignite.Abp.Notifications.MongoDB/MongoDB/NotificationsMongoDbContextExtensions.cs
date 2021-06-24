using System;
using Volo.Abp;
using Volo.Abp.MongoDB;

namespace Dignite.Abp.Notifications.MongoDB
{
    public static class NotificationsMongoDbContextExtensions
    {
        public static void ConfigureNotifications(
            this IMongoModelBuilder builder,
            Action<AbpMongoModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new NotificationsMongoModelBuilderConfigurationOptions(
                NotificationsDbProperties.DbTablePrefix
            );

            optionsAction?.Invoke(options);
        }
    }
}