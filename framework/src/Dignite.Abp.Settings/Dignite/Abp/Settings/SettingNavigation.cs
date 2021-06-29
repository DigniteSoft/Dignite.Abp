using Volo.Abp;
using Volo.Abp.Localization;

namespace Dignite.Abp.Settings
{
    /// <summary>
    /// Setting Navigation Info
    /// </summary>
    public class SettingNavigation
    {
        public string Name { get; }


        public ILocalizableString DisplayName
        {
            get => _displayName;
            set => _displayName = Check.NotNull(value, nameof(value));
        }
        private ILocalizableString _displayName;


        public SettingNavigation(string name, ILocalizableString displayName=null)
        {
            Name = name;
            DisplayName = displayName ?? new FixedLocalizableString(name);
        }
    }
}
