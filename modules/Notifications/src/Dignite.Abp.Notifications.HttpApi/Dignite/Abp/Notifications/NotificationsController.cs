using Dignite.Abp.Notifications.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Dignite.Abp.Notifications
{
    public abstract class NotificationsController : AbpController
    {
        protected NotificationsController()
        {
            LocalizationResource = typeof(DigniteAbpNotificationsResource);
        }
    }
}
