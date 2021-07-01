using System.Collections.Generic;
using System.Collections.Immutable;
using Volo.Abp;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace Dignite.Abp.Settings
{
    /// <summary>
    /// Setting Navigation Info
    /// </summary>
    public class SettingNavigation
    {
        public string Name { get; }


        private ILocalizableString _displayName;
        public ILocalizableString DisplayName
        {
            get => _displayName;
            set => _displayName = Check.NotNull(value, nameof(value));
        }
        public SettingNavigation(string name, ILocalizableString displayName=null)
        {
            Name = name;
            DisplayName = displayName ?? new FixedLocalizableString(name);
        }

        public IReadOnlyList<SettingDefinition> SettingDefinitions { get; private set; }

        public void AddSettingDefinitions(Dictionary<string, Volo.Abp.Settings.SettingDefinition> settings)
        {
            SettingDefinitions = settings.Values.ToImmutableList();
        }
    }
}
