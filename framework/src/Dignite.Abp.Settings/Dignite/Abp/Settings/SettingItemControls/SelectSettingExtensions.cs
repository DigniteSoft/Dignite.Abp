using Dignite.Abp.FieldCustomizing.Fields;
using Dignite.Abp.FieldCustomizing.Fields.Select;
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
            var config = new SelectConfiguration(new FieldConfigurationDictionary());
            configureAction(config);

            settingDefinition.WithProperty(SettingDefinitionPropertiesNames.ConfigurationName, config.GetConfiguration());
            settingDefinition.WithProperty(SettingDefinitionPropertiesNames.ProviderName, SelectFieldProvider.ProviderName);

            return settingDefinition;
        }
    }
}
