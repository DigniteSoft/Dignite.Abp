using Dignite.Abp.Settings;
using Dignite.Abp.FieldCustomizing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.SettingManagement;
using Volo.Abp.Settings;

namespace Dignite.Abp.SettingManagement
{
    [Authorize(SettingManagementPermissions.Tenant)]
    public class TenantSettingsAppService : SettingsAppServiceBase, IGlobalSettingsAppService
    {
        public TenantSettingsAppService(
            ISettingManager settingManager,
            IEnumerable<ISettingNavigationProvider> navigationProviders, 
            IEnumerable<ICustomizeFieldFormProvider> formProviders)
            :base(settingManager,navigationProviders, formProviders)
        {
        }

        protected override ISettingValueProvider SettingValueProvider
        {
            get
            {
                using (var scope = ServiceProvider.CreateScope())
                {
                    var provider = scope.ServiceProvider
                        .GetRequiredService(typeof(TenantSettingValueProvider))
                        .As<ISettingValueProvider>();

                    return provider;
                }
            }
        }

        protected override async Task UpdateAsync(string name, string value)
        {
            await _settingManager.SetForCurrentTenantAsync(name, value);
        }

        protected override async Task<List<SettingValue>> GetSettingValues()
        {
            return await _settingManager.GetAllForCurrentTenantAsync();
        }
    }
}
