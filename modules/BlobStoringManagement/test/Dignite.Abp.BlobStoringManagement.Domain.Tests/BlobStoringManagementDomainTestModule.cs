using Dignite.Abp.BlobStoringManagement.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Dignite.Abp.BlobStoringManagement
{
    /* Domain tests are configured to use the EF Core provider.
     * You can switch to MongoDB, however your domain tests should be
     * database independent anyway.
     */
    [DependsOn(
        typeof(BlobStoringManagementEntityFrameworkCoreTestModule)
        )]
    public class BlobStoringManagementDomainTestModule : AbpModule
    {
        
    }
}
