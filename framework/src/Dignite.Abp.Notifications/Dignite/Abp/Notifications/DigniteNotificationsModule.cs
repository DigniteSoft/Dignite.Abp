using Dignite.Abp.Notifications.Localization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Volo.Abp.Authorization;
using Volo.Abp.Authorization.Localization;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Features;
using Volo.Abp.Guids;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.Timing;
using Volo.Abp.VirtualFileSystem;

namespace Dignite.Abp.Notifications
{
    [DependsOn(
        typeof(AbpAuthorizationModule),
        typeof(AbpFeaturesModule),
        typeof(AbpTimingModule),
        typeof(AbpBackgroundJobsAbstractionsModule),
        typeof(AbpGuidsModule)
        )]
    public class DigniteNotificationsModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            AutoAddDefinitionProviders(context.Services);
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<AbpAuthorizationResource>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<AbpAuthorizationResource>("en")
                    .AddVirtualJson("/Dignite/Abp/Notifications/Localization");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("Dignite.Abp.Notifications", typeof(DigniteNotificationsResource));
            });
        }


        private static void AutoAddDefinitionProviders(IServiceCollection services)
        {
            var definitionProviders = new List<Type>();

            services.OnRegistred(context =>
            {
                if (typeof(INotificationDefinitionProvider).IsAssignableFrom(context.ImplementationType))
                {
                    definitionProviders.Add(context.ImplementationType);
                }
            });

            services.Configure<NotificationOptions>(options =>
            {
                options.DefinitionProviders.AddIfNotContains(definitionProviders);
            });
        }
    }
}
