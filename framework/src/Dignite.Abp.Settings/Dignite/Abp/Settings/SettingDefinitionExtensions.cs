using Dignite.Abp.FieldCustomizing.FieldControls;
using System.Collections.Generic;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace Dignite.Abp.Settings
{
    public static class SettingDefinitionExtensions
    {
        public static FieldControlConfigurationDictionary GetFieldControlConfigurationOrNull(
            this SettingDefinition setting)
        {
            var controlConfiguration = setting.Properties.GetOrDefault(SettingDefinitionPropertiesNames.ControlConfigurationName);
            if (controlConfiguration == null)
                return null;
            else
                return (FieldControlConfigurationDictionary)controlConfiguration;
        }

        public static string GetFieldControlProviderNameOrNull(
            this SettingDefinition setting)
        {
            var providerName = setting.Properties.GetOrDefault(SettingDefinitionPropertiesNames.ControlProviderName);
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
    }
}
