using System.Threading.Tasks;

namespace Dignite.Abp.BlobStoring
{
    public interface IAuthorizationHandler
    {
        Task CheckSavingPermissionAsync(AuthorizationHandlerConfiguration configuration);

        Task CheckGettingPermissionAsync(AuthorizationHandlerConfiguration configuration, IBlobInfo blobInfo);

        Task CheckDeletingPermissionAsync(AuthorizationHandlerConfiguration configuration, IBlobInfo blobInfo);
    }
}
