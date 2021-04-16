using Volo.Abp.Localization;

namespace Dignite.Abp.Settings
{
    public class SettingGroup
    {
        internal SettingGroup(string name, ILocalizableString displayName, ILocalizableString discription=null)
        {
            Name = name;
            DisplayName = displayName;
            Discription = discription;
        }


        public string Name { get; }

        public ILocalizableString DisplayName { get; set; }

        public ILocalizableString Discription { get; set; }
    }
}
