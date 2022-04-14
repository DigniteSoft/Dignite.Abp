using Dignite.Abp.FieldCustomizing.FieldControls;
using Dignite.Abp.FieldCustomizing.FieldControls.Select;
using System;
using Volo.Abp.Settings;

namespace Dignite.Abp.Settings.SettingItemControls
{
    public static class SelectSettingExtensions
    {
        public static SettingDefinition UseSelect(
            this SettingDefinition settingDefinition,
            Action<SelectConfiguration> configureAction)
        {
            var config = new SelectConfiguration(new FieldControlConfigurationDictionary());
            configureAction(config);

            settingDefinition.WithProperty(SettingDefinitionPropertiesNames.ControlConfigurationName, config);
            settingDefinition.WithProperty(SettingDefinitionPropertiesNames.ControlProviderName, SelectFieldControlProvider.ProviderName);

            return settingDefinition;
        }
    }
}
