using Dignite.Abp.FieldCustomizing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.SettingManagement;
using Volo.Abp.Settings;
using ISettingDefinitionManager = Dignite.Abp.Settings.ISettingDefinitionManager;

namespace Dignite.Abp.SettingManagement
{
    [Authorize]
    public class UserSettingsAppService : SettingsAppServiceBase, IGlobalSettingsAppService
    {
        public UserSettingsAppService(
            ISettingDefinitionManager settingDefinitionManager,
            ISettingManager settingManager,
            IEnumerable<IFormProvider> formProviders)
            : base(settingDefinitionManager, settingManager, formProviders)
        {
        }


        protected override async Task UpdateAsync(string name, string value)
        {
            await SettingManager.SetForCurrentUserAsync(name, value);
        }

        protected override async Task<List<SettingValue>> GetSettingValues()
        {
            return await SettingManager.GetAllForCurrentUserAsync();
        }
    }
}
