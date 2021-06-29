using Dignite.Abp.FieldCustomizing;
using System;
using System.Collections.Generic;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace Dignite.Abp.Settings
{
    public static class SettingDefinitionExtensions
    {
        public static SettingDefinition SetForm(
            this SettingDefinition setting,
            Action<CustomizeFieldFormConfiguration> form,
            ILocalizableString groupName=null
            )
        {
            setting.WithProperty(SettingDefinitionPropertiesNames.FormName, form);
            if (groupName != null)
            {
                setting.WithProperty(SettingDefinitionPropertiesNames.GroupName, groupName);
            }

            return setting;
        }

        public static CustomizeFieldFormConfiguration GetForm(
            this SettingDefinition setting)
        {
            return (CustomizeFieldFormConfiguration)setting.Properties.GetOrDefault(SettingDefinitionPropertiesNames.FormName);
        }

        public static ILocalizableString GetGroup(
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
