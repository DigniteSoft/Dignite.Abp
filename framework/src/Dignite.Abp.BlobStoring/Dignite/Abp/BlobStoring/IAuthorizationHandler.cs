using System.Threading.Tasks;
using Volo.Abp.BlobStoring;

namespace Dignite.Abp.BlobStoring
{
    public interface IAuthorizationHandler
    {
        Task CheckAsync(AuthorizationOperations operation, BlobContainerConfiguration containerConfiguration);
    }
}
