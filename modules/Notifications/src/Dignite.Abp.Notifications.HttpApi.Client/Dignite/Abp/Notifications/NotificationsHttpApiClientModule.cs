using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace Dignite.Abp.Notifications
{
    [DependsOn(
        typeof(NotificationsApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class NotificationsHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = NotificationsRemoteServiceConsts.RemoteServiceName;

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(NotificationsApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
