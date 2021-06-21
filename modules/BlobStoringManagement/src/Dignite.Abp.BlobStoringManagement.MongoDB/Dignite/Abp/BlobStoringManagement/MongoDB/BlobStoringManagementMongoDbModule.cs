using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;

namespace Dignite.Abp.BlobStoringManagement.MongoDB
{
    [DependsOn(
        typeof(BlobStoringManagementDomainModule),
        typeof(AbpMongoDbModule)
        )]
    public class BlobStoringManagementMongoDbModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddMongoDbContext<BlobStoringManagementMongoDbContext>(options =>
            {
                options.AddRepository<Blob, MongoBlobRepository>();
                 
            });
        }
    }
}
