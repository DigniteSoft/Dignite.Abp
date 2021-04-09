using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace Dignite.Abp.BlobStoring
{

    [DependsOn(
        typeof(DigniteAbpBlobStoringSharedModule),
        typeof(AbpDddApplicationModule)
        )]
    public class DigniteAbpBlobStoringContractsModule : AbpModule
    {
    }
}
