using Microsoft.AspNetCore.Mvc;

namespace Dignite.Abp.SettingManagement.HttpApi
{
    [Route("api/setting-management/global-settings")]
    public class GlobalSettingsController : SettingsControllerBase
    {
        public GlobalSettingsController(IGlobalSettingsAppService globalSettingsAppService)
        {
            this.SettingsAppService = globalSettingsAppService;
        }
    }
}
