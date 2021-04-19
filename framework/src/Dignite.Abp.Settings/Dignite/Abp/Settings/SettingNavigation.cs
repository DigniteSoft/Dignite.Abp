
using Volo.Abp;
using Volo.Abp.Localization;

namespace Dignite.Abp.Settings
{
    
    public class SettingNavigation
    {
        public string Name { get; }


        public ILocalizableString DisplayName
        {
            get => _displayName;
            set => _displayName = Check.NotNull(value, nameof(value));
        }
        private ILocalizableString _displayName;

        public string Icon { get; set; }


        public SettingNavigation(string name, ILocalizableString displayName=null, string icon=null)
        {
            Name = name;
            DisplayName = displayName ?? new FixedLocalizableString(name);
            Icon = icon;
        }
    }
}
