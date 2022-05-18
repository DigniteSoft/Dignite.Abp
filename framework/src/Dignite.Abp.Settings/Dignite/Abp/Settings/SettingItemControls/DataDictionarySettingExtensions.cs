using Dignite.Abp.FieldCustomizing.FieldControls;
using Dignite.Abp.FieldCustomizing.FieldControls.DataDictionary;
using System;
using Volo.Abp.Settings;

namespace Dignite.Abp.Settings.SettingItemControls
{
    public static class DataDictionarySettingExtensions
    {
        public static SettingDefinition UseDataDictionary(
            this SettingDefinition settingDefinition,
            Action<DataDictionaryConfiguration> dataDictionaryConfigureAction)
        {
            var dataDictionaryConfiguration = new DataDictionaryConfiguration(new FieldControlConfigurationDictionary());
            dataDictionaryConfigureAction(dataDictionaryConfiguration);

            settingDefinition.WithProperty(SettingDefinitionPropertiesNames.ControlConfigurationName, dataDictionaryConfiguration.GetConfiguration());
            settingDefinition.WithProperty(SettingDefinitionPropertiesNames.ControlProviderName, DataDictionaryFieldControlProvider.ProviderName);

            return settingDefinition;
        }
    }
}
