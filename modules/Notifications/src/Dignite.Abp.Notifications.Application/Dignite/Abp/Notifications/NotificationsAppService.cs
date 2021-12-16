using Dignite.Abp.Notifications.Localization;
using Volo.Abp.Application.Services;

namespace Dignite.Abp.Notifications
{
    public abstract class NotificationsAppService : ApplicationService
    {
        protected NotificationsAppService()
        {
            LocalizationResource = typeof(NotificationResource);
            ObjectMapperContext = typeof(NotificationsApplicationModule);
        }
    }
}
