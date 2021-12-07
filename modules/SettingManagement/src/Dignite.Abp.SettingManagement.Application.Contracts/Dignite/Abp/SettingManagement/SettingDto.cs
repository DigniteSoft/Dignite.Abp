
using Dignite.Abp.FieldCustomizing.FieldControls;

namespace Dignite.Abp.SettingManagement
{
    public class SettingDto
    {
        public SettingDto(
            string groupName, 
            string name, 
            string displayName, 
            string description, 
            string value,
            string fieldControlProviderName,
            FieldControlConfigurationBase fieldControlConfiguration)
        {
            GroupName = groupName;
            Name = name;
            DisplayName = displayName;
            Description = description;
            Value = value;
            FieldControlProviderName = fieldControlProviderName;
            FieldControlConfiguration = fieldControlConfiguration;
        }

        public string GroupName { get;  }

        public string Name { get; }
        public string DisplayName { get; }
        public string Description { get; }

        public string Value { get;  }

        public string FieldControlProviderName { get; }

        public FieldControlConfigurationBase FieldControlConfiguration { get; }
    }
}
