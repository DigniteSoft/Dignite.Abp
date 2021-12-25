using Dignite.Abp.NotificationCenter.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Dignite.Abp.NotificationCenter.Pages
{
    public abstract class NotificationCenterPageModel : AbpPageModel
    {
        protected NotificationCenterPageModel()
        {
            LocalizationResourceType = typeof(NotificationCenterResource);
        }
    }
}