using Dignite.Apps.ConfigurationStore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;

namespace Dignite.Apps
{
    [DependsOn(
        typeof(AbpMultiTenancyModule)
        )]
    public class DigniteAppsModule:AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            Configure<DigniteDefaultAppStoreOptions>(configuration);
        }
    }
}
