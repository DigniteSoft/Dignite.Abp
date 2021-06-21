using Volo.Abp.Modularity;

namespace Dignite.Abp.BlobStoringManagement
{
    [DependsOn(
        typeof(BlobStoringManagementApplicationModule),
        typeof(BlobStoringManagementDomainTestModule)
        )]
    public class BlobStoringManagementApplicationTestModule : AbpModule
    {

    }
}
