using Dignite.FieldCustomizing;
using System;
using System.Collections.Generic;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace Dignite.Abp.Settings
{
    public static class SettingDefinitionExtensions
    {
        public static SettingDefinition SetField(
            this SettingDefinition setting,
            Action<CustomizeFieldConfiguration> fieldConfiguration,
            ILocalizableString groupName=null
            )
        {
            if (groupName != null)
            {
                setting.WithProperty(SettingDefinitionPropertiesNames.Group, groupName);
            }
            setting.WithProperty(SettingDefinitionPropertiesNames.FieldConfiguration, fieldConfiguration);

            return setting;
        }

        public static CustomizeFieldConfiguration GetField(
            this SettingDefinition setting)
        {
            return (CustomizeFieldConfiguration)setting.Properties.GetOrDefault(SettingDefinitionPropertiesNames.FieldConfiguration);
        }

        public static ILocalizableString GetGroup(
            this SettingDefinition setting)
        {
            var group = setting.Properties.GetOrDefault(SettingDefinitionPropertiesNames.Group);
            if (group != null)
            {
                return (ILocalizableString)group;
            }
            return null;
        }
    }
}
