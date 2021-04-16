using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Settings;

namespace Dignite.Abp.Settings
{
    public class SettingDefinitionManager<T> : ISettingDefinitionManager<T>, ISingletonDependency
        where T : ISettingDefinitionProvider
    {
        protected AbpSettingOptions Options { get; }

        protected IServiceProvider ServiceProvider { get; }

        public virtual IReadOnlyList<SettingDefinition> GetAll()
        {
            var settings = new Dictionary<string, SettingDefinition>();

            using (var scope = ServiceProvider.CreateScope())
            {
                var provider = scope.ServiceProvider
                    .GetRequiredService(typeof(T))
                    .As<ISettingDefinitionProvider>();

                provider.Define(new SettingDefinitionContext(settings));
            }

            return settings.Values.ToImmutableList();
        }
    }
}
