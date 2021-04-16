using Dignite.FieldCustomizing;
using Volo.Abp.Settings;

namespace Dignite.Abp.Settings
{
    public class SettingFieldConfigurationProvider : IFieldConfigurationProvider
    {
        private readonly ISettingDefinitionManager _settingManager;

        public SettingFieldConfigurationProvider(ISettingDefinitionManager settingManager)
        {
            _settingManager = settingManager;
        }

        public FieldConfiguration Get(string name)
        {
            var setting = _settingManager.Get(name);
            return setting.GetField();
        }
    }
}
