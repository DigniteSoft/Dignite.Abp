
using Dignite.FieldCustomizing;

namespace Dignite.Abp.SettingManagement
{
    public class SettingDto
    {
        public SettingDto(string groupName, string name, string displayName, string description, 
            string value,
            string fieldProviderName, 
            FieldProviderConfigurationBase fieldProviderConfiguration)
        {
            GroupName = groupName;
            Name = name;
            DisplayName = displayName;
            Description = description;
            Value = value;
            FieldProviderName = fieldProviderName;
            FieldProviderConfiguration = fieldProviderConfiguration;
        }

        public string GroupName { get;  }

        public string Name { get; }
        public string DisplayName { get; }
        public string Description { get; }

        public string Value { get;  }

        public string FieldProviderName { get; }

        public FieldProviderConfigurationBase FieldProviderConfiguration { get; }
    }
}
