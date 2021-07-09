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
            Action<FormConfigurationData> formConfigurationAction,
            ILocalizableString groupName =null
            )
        {
            var formConfiguration = new FormConfigurationData();
            formConfigurationAction(formConfiguration);

            setting.WithProperty(SettingDefinitionPropertiesNames.FormName, formConfiguration);
            if (groupName != null)
            {
                setting.WithProperty(SettingDefinitionPropertiesNames.FormName, groupName);
            }

            return setting;
        }

        public static FormConfigurationData GetFormOrNull(
            this SettingDefinition setting)
        {
            var formConfiguration = setting.Properties.GetOrDefault(SettingDefinitionPropertiesNames.FormName);
            if (formConfiguration == null)
                return null;
            else
                return (FormConfigurationData)formConfiguration;
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
