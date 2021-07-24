using Dignite.Abp.FieldCustomizing;
using Dignite.Abp.Settings.Localization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Dignite.Abp.Settings
{
    [DependsOn(
        typeof(Volo.Abp.Settings.AbpSettingsModule),
        typeof(DigniteAbpFieldCustomizingModule)
        )]
    public class DigniteAbpSettingsModule:AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            AutoAddDefinitionProviders(context.Services);
        }


        private static void AutoAddDefinitionProviders(IServiceCollection services)
        {
            var definitionProviders = new List<Type>();

            services.OnRegistred(context =>
            {
                if (typeof(IDigniteSettingDefinitionProvider).IsAssignableFrom(context.ImplementationType))
                {
                    definitionProviders.Add(context.ImplementationType);
                }
            });

            services.Configure<DigniteSettingOptions>(options =>
            {
                options.DigniteDefinitionProviders.AddIfNotContains(definitionProviders);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<DigniteAbpSettingsModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<DigniteAbpSettingsResource>("en")
                    .AddBaseTypes(typeof(FieldCustomizing.Localization.DigniteAbpFieldCustomizingResource))
                    .AddVirtualJson("/Dignite/Abp/Settings/Localization/Resources");
            });
        }
    }
}
