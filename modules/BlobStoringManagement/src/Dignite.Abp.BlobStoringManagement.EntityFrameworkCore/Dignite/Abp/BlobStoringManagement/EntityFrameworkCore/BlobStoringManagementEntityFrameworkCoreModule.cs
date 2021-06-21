using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Dignite.Abp.BlobStoringManagement.EntityFrameworkCore
{
    [DependsOn(
        typeof(BlobStoringManagementDomainModule),
        typeof(AbpEntityFrameworkCoreModule)
    )]
    public class BlobStoringManagementEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<BlobStoringManagementDbContext>(options =>
            {
                options.AddRepository<Blob, EfCoreBlobRepository>();
                 
            });
        }
    }
}