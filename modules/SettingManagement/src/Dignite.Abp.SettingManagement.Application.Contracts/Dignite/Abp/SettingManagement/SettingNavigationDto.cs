

namespace Dignite.Abp.SettingManagement
{
    public class SettingNavigationDto
    {
        public SettingNavigationDto(string name, string displayName, string icon)
        {
            Name = name;
            DisplayName = displayName;
            Icon = icon;
        }

        public string Name { get; }

        public string DisplayName { get;  }

        public string Icon { get;  }
    }
}
