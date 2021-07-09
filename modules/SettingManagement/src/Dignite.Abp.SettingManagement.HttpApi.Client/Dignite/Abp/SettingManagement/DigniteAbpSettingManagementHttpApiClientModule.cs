using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace Dignite.Abp.SettingManagement
{
    [DependsOn(
        typeof(DigniteAbpSettingManagementApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class DigniteAbpSettingManagementHttpApiClientModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(DigniteAbpSettingManagementApplicationContractsModule).Assembly,
                SettingManagementRemoteServiceConsts.RemoteServiceName
            );
        }
    }
}
