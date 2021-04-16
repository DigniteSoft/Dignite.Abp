using System.Collections.Generic;
using Volo.Abp.Localization;

namespace Dignite.Abp.Settings
{
    public class SettingNavigation
    {
        public string Name { get; }

        public ILocalizableString DisplayName { get; set; }

        public string Icon { get; set; }

        public List<SettingGroup> SettingGroups { get; }

        public SettingNavigation(string name)
        {
            Name = name;
        }

        public SettingNavigation AddSettingGroup(
            string name,
            ILocalizableString displayName,
            ILocalizableString description=null
            )
        {
            SettingGroups.Add(new SettingGroup(name, displayName, description));

            return this;
        }
    }
}
