using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Dignite.Abp.Notifications.EntityFrameworkCore
{
    public class NotificationsModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public NotificationsModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "",
            [CanBeNull] string schema = null)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}