using Dignite.Abp.FieldCustomizing.FieldControls;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.SettingManagement;
using Volo.Abp.Settings;
using ISettingDefinitionManager = Dignite.Abp.Settings.IDigniteSettingDefinitionManager;

namespace Dignite.Abp.SettingManagement
{
    [Authorize]
    public class UserSettingsAppService : SettingsAppServiceBase, IGlobalSettingsAppService
    {
        public UserSettingsAppService(
            ISettingDefinitionManager settingDefinitionManager,
            ISettingManager settingManager,
            IEnumerable<IFieldControlProvider> controlProviders)
            : base(settingDefinitionManager, settingManager, controlProviders)
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
