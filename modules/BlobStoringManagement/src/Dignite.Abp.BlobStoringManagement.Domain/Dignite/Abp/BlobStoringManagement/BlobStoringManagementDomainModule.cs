using Dignite.Abp.BlobStoring;
using Volo.Abp.BlobStoring;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Dignite.Abp.BlobStoringManagement
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(BlobStoringManagementDomainSharedModule)
    )]
    public class BlobStoringManagementDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {

            Configure<AbpBlobStoringOptions>(options =>
            {
                options.Containers.ConfigureDefault(container =>
                {
                    container.SetBlobInfoStore<BlobStore>();
                });
            });
        }
    }
}
