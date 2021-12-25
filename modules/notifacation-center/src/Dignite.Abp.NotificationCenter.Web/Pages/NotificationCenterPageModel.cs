using Dignite.Abp.NotificationCenter.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Dignite.Abp.NotificationCenter.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class NotificationCenterPageModel : AbpPageModel
    {
        protected NotificationCenterPageModel()
        {
            LocalizationResourceType = typeof(NotificationCenterResource);
            ObjectMapperContext = typeof(NotificationCenterWebModule);
        }
    }
}