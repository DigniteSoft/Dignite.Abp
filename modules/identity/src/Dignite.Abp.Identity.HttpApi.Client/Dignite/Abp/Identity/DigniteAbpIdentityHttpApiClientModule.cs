using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Dignite.Abp.Identity
{
    [DependsOn(
      typeof(AbpIdentityHttpApiClientModule),
      typeof(DigniteAbpIdentityApplicationContractsModule),
      typeof(AbpHttpClientModule))]
    public class DigniteAbpIdentityHttpApiClientModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddStaticHttpClientProxies(
                typeof(DigniteAbpIdentityApplicationContractsModule).Assembly,
                Volo.Abp.Identity.IdentityRemoteServiceConsts.RemoteServiceName
            );

            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<DigniteAbpIdentityHttpApiClientModule>();
            });
        }
    }
}
