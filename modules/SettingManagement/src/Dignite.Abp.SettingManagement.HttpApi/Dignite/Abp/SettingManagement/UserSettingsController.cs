using Microsoft.AspNetCore.Mvc;

namespace Dignite.Abp.SettingManagement.HttpApi
{
    [Route("api/setting-management/user-settings")]
    public class UserSettingsController : SettingsControllerBase
    {
        public UserSettingsController(IUserSettingsAppService userSettingsAppService)
        {
            this.SettingsAppService = userSettingsAppService;
        }
    }
}
