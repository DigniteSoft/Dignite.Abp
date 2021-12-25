using Dignite.Abp.NotificationCenter.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Dignite.Abp.NotificationCenter.Blazor.Server.Host
{
    public abstract class NotificationCenterComponentBase : AbpComponentBase
    {
        protected NotificationCenterComponentBase()
        {
            LocalizationResource = typeof(NotificationCenterResource);
        }
    }
}
