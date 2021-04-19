using Dignite.FieldCustomizing;
using System;
using System.Collections.Generic;
using Volo.Abp.Settings;

namespace Dignite.Abp.Settings
{
    public static class SettingDefinitionExtensions
    {
        public static SettingDefinition SetField(
            this SettingDefinition setting,
            SettingGroup group,
            Action<FieldConfiguration> fieldConfiguration
            )
        {
            setting.WithProperty(SettingDefinitionPropertiesNames.Group, group);
            setting.WithProperty(SettingDefinitionPropertiesNames.FieldConfiguration, fieldConfiguration);

            return setting;
        }

        public static FieldConfiguration GetField(
            this SettingDefinition setting)
        {
            return (FieldConfiguration)setting.Properties.GetOrDefault(SettingDefinitionPropertiesNames.FieldConfiguration);
        }

        public static SettingGroup GetGroup(
            this SettingDefinition setting)
        {
            return (SettingGroup)setting.Properties.GetOrDefault(SettingDefinitionPropertiesNames.Group);
        }
    }
}
