using Dignite.Abp.FieldCustomizing.Fields;
using System.Collections.Generic;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace Dignite.Abp.Settings
{
    public static class SettingDefinitionExtensions
    {
        public static FieldConfigurationDictionary GetFieldConfigurationOrNull(
            this SettingDefinition setting)
        {
            var controlConfiguration = setting.Properties.GetOrDefault(SettingDefinitionPropertiesNames.ConfigurationName);
            if (controlConfiguration == null)
                return null;
            else
                return (FieldConfigurationDictionary)controlConfiguration;
        }

        public static string GetFieldProviderNameOrNull(
            this SettingDefinition setting)
        {
            var providerName = setting.Properties.GetOrDefault(SettingDefinitionPropertiesNames.ProviderName);
            if (providerName != null)
            {
                return (string)providerName;
            }
            return null;
        }

        public static ILocalizableString GetGroupOrNull(
            this SettingDefinition setting)
        {
            var group = setting.Properties.GetOrDefault(SettingDefinitionPropertiesNames.GroupName);
            if (group != null)
            {
                return (ILocalizableString)group;
            }
            return null;
        }

        public static void SetGroup(
            this SettingDefinition setting,
            ILocalizableString group)
        {
            setting.Properties.Add(SettingDefinitionPropertiesNames.GroupName, group);
        }
    }
}
