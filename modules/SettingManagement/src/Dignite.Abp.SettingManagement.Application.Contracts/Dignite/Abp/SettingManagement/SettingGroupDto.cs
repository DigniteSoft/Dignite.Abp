using System.Collections.Generic;

namespace Dignite.Abp.SettingManagement
{
    public class SettingGroupDto
    {
        public SettingGroupDto(string displayName, string discription, IReadOnlyList<SettingDto> settings)
        {
            DisplayName = displayName;
            Discription = discription;
            Settings = settings;
        }

        public string DisplayName { get; set; }

        public string Discription { get; set; }

        public IReadOnlyList<SettingDto> Settings { get; set; }
    }
}
