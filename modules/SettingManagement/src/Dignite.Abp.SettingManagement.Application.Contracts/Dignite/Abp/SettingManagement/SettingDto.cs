
using Dignite.FieldCustomizing;

namespace Dignite.Abp.SettingManagement
{
    public class SettingDto
    {
        public SettingDto(string name, string displayName, string description, 
            string value,
            string fieldProviderName, 
            FieldProviderConfigurationBase fieldProviderConfiguration)
        {
            Name = name;
            DisplayName = displayName;
            Description = description;
            Value = value;
            FieldProviderName = fieldProviderName;
            FieldProviderConfiguration = fieldProviderConfiguration;
        }

        public string Name { get; }
        public string DisplayName { get; }
        public string Description { get; }

        public string Value { get;  }

        public string FieldProviderName { get; }

        public FieldProviderConfigurationBase FieldProviderConfiguration { get; }
    }
}
