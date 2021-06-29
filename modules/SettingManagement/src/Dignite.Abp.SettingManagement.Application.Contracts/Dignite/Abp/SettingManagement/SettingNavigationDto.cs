

namespace Dignite.Abp.SettingManagement
{
    public class SettingNavigationDto
    {
        public SettingNavigationDto(string name, string displayName)
        {
            Name = name;
            DisplayName = displayName;
        }

        public string Name { get; }

        public string DisplayName { get;  }
    }
}
