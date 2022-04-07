using Dignite.Abp.FieldCustomizing.FieldControls;

namespace Dignite.Abp.SettingManagement
{
    public class SettingDto
    {
        public SettingDto(
            string group, 
            string name, 
            string displayName, 
            string description, 
            string value,
            string fieldControlProviderName,
            FieldControlConfigurationDictionary fieldControlConfiguration)
        {
            Group = group;
            Name = name;
            DisplayName = displayName;
            Description = description;
            Value = value;
            FieldControlProviderName = fieldControlProviderName;
            FieldControlConfiguration = fieldControlConfiguration;
        }

        public string Group { get;  }

        public string Name { get; }
        public string DisplayName { get; }
        public string Description { get; }

        public string Value { get;  }

        public string FieldControlProviderName { get; }

        public FieldControlConfigurationDictionary FieldControlConfiguration { get; }
    }
}
