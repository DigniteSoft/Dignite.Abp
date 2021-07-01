using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.Settings
{
    public class SettingDefinitionManager: Volo.Abp.Settings.SettingDefinitionManager, ISettingDefinitionManager, ISingletonDependency
    {
        protected Lazy<IList<SettingNavigation>> Navigations { get; }
        protected new AbpSettingOptions Options { get; }
        public SettingDefinitionManager(
            IOptions<AbpSettingOptions> options,
            IServiceProvider serviceProvider)
            :base(options,serviceProvider)
        {
            Options = options.Value;
            Navigations = new Lazy<IList<SettingNavigation>>(CreateSettingNavigations, true);
        }

        public virtual IList<SettingNavigation> GetNavigations()
        {
            return Navigations.Value;
        }


        public virtual SettingNavigation GetNavigation(string navigationName)
        {
            Check.NotNull(navigationName, nameof(navigationName));

            var navigation = GetNavigationOrNull(navigationName);

            if (navigation == null)
            {
                throw new AbpException("Undefined setting: " + navigationName);
            }

            return navigation;
        }

        public virtual SettingNavigation GetNavigationOrNull(string navigationName)
        {
            return Navigations.Value.SingleOrDefault(n => n.Name == navigationName);
        }               

        protected virtual IList<SettingNavigation> CreateSettingNavigations()
        {
            var navigations = new List<SettingNavigation>();
            using (var scope = ServiceProvider.CreateScope())
            {
                var providers = Options
                    .DefinitionProviders
                    .Select(p => scope.ServiceProvider.GetRequiredService(p) as ISettingDefinitionProvider)
                    .ToList();

                foreach (var provider in providers)
                {
                    var settings = new Dictionary<string, Volo.Abp.Settings.SettingDefinition>();
                    var context = new SettingDefinitionContext(settings);
                    provider.Define(context);
                    context.Navigation.AddSettingDefinitions(settings);
                    navigations.Add(context.Navigation);
                }
            }

            return navigations;
        }
    }
}
