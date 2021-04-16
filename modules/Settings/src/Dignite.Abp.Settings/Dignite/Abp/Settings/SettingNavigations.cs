using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Volo.Abp;

namespace Dignite.Abp.Settings
{
    public class SettingNavigations
    {

        private readonly Dictionary<string, SettingNavigation> _navigations;

        public SettingNavigations()
        {
            _navigations = new Dictionary<string, SettingNavigation>();
        }

        public SettingNavigations Configure<TContainer>(
            Action<SettingNavigation> configureAction)
        {
            return Configure(
                SettingNavigationNameAttribute.GetNavigationName<TContainer>(),
                configureAction
            );
        }

        public SettingNavigations Configure(
            [NotNull] string name,
            [NotNull] Action<SettingNavigation> configureAction)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.NotNull(configureAction, nameof(configureAction));

            configureAction(
                _navigations.GetOrAdd(
                    name,
                    () => new SettingNavigation(name)
                )
            );

            return this;
        }


        public SettingNavigation GetConfiguration<TContainer>()
        {
            return GetConfiguration(SettingNavigationNameAttribute.GetNavigationName<TContainer>());
        }

        public SettingNavigation GetConfiguration([NotNull] string name)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            return _navigations.GetOrDefault(name);
        }

        public IReadOnlyList<SettingNavigation> GetAll()
        {
            return _navigations.Values.ToImmutableList();
        }
    }
}