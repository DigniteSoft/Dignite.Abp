using Dignite.Abp.Settings;
using Dignite.FieldCustomizing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.SettingManagement;
using Volo.Abp.Settings;

namespace Dignite.Abp.SettingManagement
{
    [Authorize(SettingManagementPermissions.Global)]
    public class GlobalSettingsAppService : SettingsAppServiceBase, IGlobalSettingsAppService
    {
        public GlobalSettingsAppService(
            ISettingManager settingManager,
            IEnumerable<ISettingNavigationProvider> navigationProviders, 
            IEnumerable<IFieldProvider> fieldProviders)
            :base(settingManager,navigationProviders, fieldProviders)
        {
        }

        protected override ISettingValueProvider SettingValueProvider
        {
            get
            {
                using (var scope = ServiceProvider.CreateScope())
                {
                    var provider = scope.ServiceProvider
                        .GetRequiredService(typeof(GlobalSettingValueProvider))
                        .As<ISettingValueProvider>();

                    return provider;
                }
            }
        }

        protected override async Task UpdateAsync(string name, string value)
        {
            await _settingManager.SetGlobalAsync(name, value);
        }

        protected override async Task<List<SettingValue>> GetSettingValues()
        {
            return await _settingManager.GetAllGlobalAsync();
        }
    }
}
