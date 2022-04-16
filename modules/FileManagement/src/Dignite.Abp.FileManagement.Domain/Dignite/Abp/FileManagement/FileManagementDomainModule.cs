using Dignite.Abp.BlobStoring;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Dignite.Abp.FileManagement
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(FileManagementDomainSharedModule),
        typeof(DigniteAbpBlobStoringAbstractionsModule)
    )]
    public class FileManagementDomainModule : AbpModule
    {
    }
}
