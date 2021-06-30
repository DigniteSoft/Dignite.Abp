using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.Settings
{
    public class SettingDefinitionManager: Volo.Abp.Settings.SettingDefinitionManager, ISettingDefinitionManager, ISingletonDependency
    {
        protected Lazy<IList<SettingNavigation>> Navigations { get; }

        public SettingDefinitionManager(
            IOptions<AbpSettingOptions> options,
            IServiceProvider serviceProvider)
            :base(options,serviceProvider)
        {
            Navigations = new Lazy<IList<SettingNavigation>>(CreateSettingNavigations, true);
        }

        public virtual IList<SettingNavigation> GetNavigations()
        {
            return Navigations.Value;
        }

        public virtual IReadOnlyList<Volo.Abp.Settings.SettingDefinition> GetAllOfNavigation(string navigationName)
        {
            var settings = new Dictionary<string, Volo.Abp.Settings.SettingDefinition>();
            var navigations = new List<SettingNavigation>();

            using (var scope = ServiceProvider.CreateScope())
            {
                var providers = Options
                    .DefinitionProviders
                    .Select(p => scope.ServiceProvider.GetRequiredService(p) as ISettingDefinitionProvider)
                    .ToList();

                foreach (var provider in providers)
                {
                    var context = new SettingDefinitionContext(settings);
                    if (context.Navigation.Name == navigationName)
                    {
                        provider.Define(context);
                    }
                }
            }

            return settings.Values.ToImmutableList();
        }

        protected virtual IList<SettingNavigation> CreateSettingNavigations()
        {
            var settings = new Dictionary<string, Volo.Abp.Settings.SettingDefinition>();
            var navigations = new List<SettingNavigation>();

            using (var scope = ServiceProvider.CreateScope())
            {
                var providers = Options
                    .DefinitionProviders
                    .Select(p => scope.ServiceProvider.GetRequiredService(p) as ISettingDefinitionProvider)
                    .ToList();

                foreach (var provider in providers)
                {
                    var context = new SettingDefinitionContext(settings);
                    provider.Define(context);
                    navigations.Add(context.Navigation);
                }
            }

            return navigations;
        }
    }
}
