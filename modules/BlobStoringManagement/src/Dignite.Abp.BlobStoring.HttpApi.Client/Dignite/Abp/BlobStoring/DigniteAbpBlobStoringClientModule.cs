using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace Dignite.Abp.BlobStoring
{
    [DependsOn(
        typeof(DigniteAbpBlobStoringContractsModule),
        typeof(AbpHttpClientModule))]
    public class DigniteAbpBlobStoringClientModule : AbpModule
    {

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(DigniteAbpBlobStoringContractsModule).Assembly,
                BlobStoringRemoteServiceConsts.RemoteServiceName
            );
        }
    }
}
