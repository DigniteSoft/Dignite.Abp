
using Dignite.Abp.FieldCustomizing;

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
            string formProviderName,
            FormConfigurationBase formConfiguration)
        {
            GroupName = groupName;
            Name = name;
            DisplayName = displayName;
            Description = description;
            Value = value;
            FormProviderName = formProviderName;
            FormConfiguration = formConfiguration;
        }

        public string GroupName { get;  }

        public string Name { get; }
        public string DisplayName { get; }
        public string Description { get; }

        public string Value { get;  }

        public string FormProviderName { get; }

        public FormConfigurationBase FormConfiguration { get; }
    }
}
