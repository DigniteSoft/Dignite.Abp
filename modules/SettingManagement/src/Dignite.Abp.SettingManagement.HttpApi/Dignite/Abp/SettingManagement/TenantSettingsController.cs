using Microsoft.AspNetCore.Mvc;

namespace Dignite.Abp.SettingManagement.HttpApi
{
    [Route("api/setting-management/tenant-settings")]
    public class TenantSettingsController : SettingsControllerBase
    {
        public TenantSettingsController(ITenantSettingsAppService tenantSettingsAppService)
        {
            this.SettingsAppService = tenantSettingsAppService;
        }
    }
}
