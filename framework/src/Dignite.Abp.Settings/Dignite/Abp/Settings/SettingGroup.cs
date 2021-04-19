using Volo.Abp;
using Volo.Abp.Localization;

namespace Dignite.Abp.Settings
{
    public class SettingGroup
    {
        internal SettingGroup(ILocalizableString displayName, ILocalizableString description=null)
        {
            DisplayName = displayName;
            Description = description;
        }



        public ILocalizableString DisplayName
        {
            get => _displayName;
            set => _displayName = Check.NotNull(value, nameof(value));
        }
        private ILocalizableString _displayName;


        public ILocalizableString Description
        {
            get => _discription;
            set => _discription = Check.NotNull(value, nameof(value));
        }
        private ILocalizableString _discription;

    }
}
