using Dignite.Abp.FieldCustomizing.Fields;
using Dignite.Abp.FieldCustomizing.Fields.Textbox;
using System;
using Volo.Abp.Settings;

namespace Dignite.Abp.Settings.SettingItemControls
{
    public static class TextboxSettingExtensions
    {
        public static SettingDefinition UseTextbox(
            this SettingDefinition settingDefinition,
            Action<TextboxConfiguration> textboxConfigureAction)
        {
            var textboxConfiguration = new TextboxConfiguration(new FieldConfigurationDictionary());
            textboxConfigureAction(textboxConfiguration);

            settingDefinition.WithProperty(SettingDefinitionPropertiesNames.ConfigurationName, textboxConfiguration.GetConfiguration());
            settingDefinition.WithProperty(SettingDefinitionPropertiesNames.ProviderName, TextboxFieldProvider.ProviderName);

            return settingDefinition;
        }
    }
}
