using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace Dignite.Abp.BlobStoringManagement
{
    [DependsOn(
        typeof(BlobStoringManagementApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class BlobStoringManagementHttpApiClientModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(BlobStoringManagementApplicationContractsModule).Assembly,
                BlobStoringManagementRemoteServiceConsts.RemoteServiceName
            );
        }
    }
}
