using Dignite.Abp.Settings.Localization;
using Volo.Abp.Application.Services;

namespace Dignite.Abp.SettingManagement
{
    public class SettingManagementAppServiceBase : ApplicationService
    {
        protected SettingManagementAppServiceBase()
        {
            ObjectMapperContext = typeof(DigniteSettingManagementApplicationModule);
            LocalizationResource = typeof(DigniteAbpSettingsResource);
        }
    }
}
