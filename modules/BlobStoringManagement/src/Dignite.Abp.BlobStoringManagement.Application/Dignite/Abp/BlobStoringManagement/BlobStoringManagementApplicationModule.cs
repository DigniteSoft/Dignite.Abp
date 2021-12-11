using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using Volo.Abp.BlobStoring;
using Dignite.Abp.BlobStoring;

namespace Dignite.Abp.BlobStoringManagement
{
    [DependsOn(
        typeof(BlobStoringManagementDomainModule),
        typeof(BlobStoringManagementApplicationContractsModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule)
        )]
    public class BlobStoringManagementApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<BlobStoringManagementApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<BlobStoringManagementApplicationModule>(validate: true);
            });


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
