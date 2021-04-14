using Volo.Abp.Authorization;
using Volo.Abp.BlobStoring;
using Volo.Abp.Modularity;

namespace Dignite.Abp.BlobStoring
{
    [DependsOn(
        typeof(DigniteAbpBlobStoringSharedModule),
        typeof(AbpAuthorizationModule),
        typeof(AbpBlobStoringModule)
        )]
    public class DigniteAbpBlobStoringModule: AbpModule
    {
    }
}
