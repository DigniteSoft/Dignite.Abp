using Dignite.Abp.FieldCustomizing.Fields;
using Dignite.Abp.FieldCustomizing.Fields.DataDictionary;
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
            var dataDictionaryConfiguration = new DataDictionaryConfiguration(new FieldConfigurationDictionary());
            dataDictionaryConfigureAction(dataDictionaryConfiguration);

            settingDefinition.WithProperty(SettingDefinitionPropertiesNames.ConfigurationName, dataDictionaryConfiguration.GetConfiguration());
            settingDefinition.WithProperty(SettingDefinitionPropertiesNames.ProviderName, DataDictionaryFieldProvider.ProviderName);

            return settingDefinition;
        }
    }
}
