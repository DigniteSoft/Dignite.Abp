using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Dignite.Abp.BlobStoringManagement
{
    [DependsOn(
        typeof(BlobStoringManagementHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class BlobStoringManagementConsoleApiClientModule : AbpModule
    {
        
    }
}
