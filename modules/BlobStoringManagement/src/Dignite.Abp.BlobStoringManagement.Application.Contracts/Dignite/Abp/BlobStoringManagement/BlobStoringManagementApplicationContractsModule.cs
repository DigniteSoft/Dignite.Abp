using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace Dignite.Abp.BlobStoringManagement
{
    [DependsOn(
        typeof(BlobStoringManagementDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
        )]
    public class BlobStoringManagementApplicationContractsModule : AbpModule
    {

    }
}
