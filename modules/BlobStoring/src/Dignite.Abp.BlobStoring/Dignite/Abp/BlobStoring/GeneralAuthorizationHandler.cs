using System.Threading.Tasks;
using Volo.Abp.BlobStoring;

namespace Dignite.Abp.BlobStoring
{
    public class GeneralAuthorizationHandler : AuthorizationHandlerBase
    {
        protected override Task CheckResourcePermissionAsync(BlobContainerConfiguration containerConfiguration)
        {
            return Task.CompletedTask;
        }
    }
}
