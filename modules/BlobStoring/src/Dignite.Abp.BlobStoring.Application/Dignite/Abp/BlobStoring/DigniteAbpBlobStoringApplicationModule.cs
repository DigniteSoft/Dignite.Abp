
using Volo.Abp.Modularity;

namespace Dignite.Abp.BlobStoring
{
    [DependsOn(
        typeof(DigniteAbpBlobStoringContractsModule),
        typeof(DigniteAbpBlobStoringModule)
    )]
    public class DigniteAbpBlobStoringApplicationModule : AbpModule
    {
    }
}
