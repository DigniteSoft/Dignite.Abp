using Dignite.Abp.Notifications.Localization;
using Localization.Resources.AbpUi;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;

namespace Dignite.Abp.Notifications
{
    [DependsOn(
        typeof(NotificationsApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class NotificationsHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(NotificationsHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<NotificationResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
