using Dignite.Abp.FieldCustomizing.FieldControls;
using Dignite.Abp.FieldCustomizing.FieldControls.Textbox;
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
            var textboxConfiguration = new TextboxConfiguration(new FieldControlConfigurationDictionary());
            textboxConfigureAction(textboxConfiguration);

            settingDefinition.WithProperty(SettingDefinitionPropertiesNames.ControlConfigurationName, textboxConfiguration.GetConfiguration());
            settingDefinition.WithProperty(SettingDefinitionPropertiesNames.ControlProviderName, TextboxFieldControlProvider.ProviderName);

            return settingDefinition;
        }
    }
}
