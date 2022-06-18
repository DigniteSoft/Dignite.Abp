using Dignite.Abp.FieldCustomizing.Fields;
using Dignite.Abp.FieldCustomizing.Fields.Switch;
using System;
using Volo.Abp.Settings;

namespace Dignite.Abp.Settings.SettingItemControls
{
    public static class SwitchSettingExtensions
    {
        public static SettingDefinition UseSwitch(
            this SettingDefinition settingDefinition,
            Action<SwitchConfiguration> textboxConfigureAction)
        {
            var configuration = new SwitchConfiguration(new FieldConfigurationDictionary());
            textboxConfigureAction(configuration);

            settingDefinition.WithProperty(SettingDefinitionPropertiesNames.ConfigurationName, configuration.GetConfiguration());
            settingDefinition.WithProperty(SettingDefinitionPropertiesNames.ProviderName, SwitchFieldProvider.ProviderName);

            return settingDefinition;
        }
    }
}
