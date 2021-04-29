using JetBrains.Annotations;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Dignite.Abp.BlobStoring
{
    public interface IBlobStoringAppService: IApplicationService
    {
        Task<string> RemoteFileSaveAsBlobAsync([NotNull] string containerName, [NotNull] string url);

        Task<string> SaveBlobAsync([NotNull] string containerName, [NotNull] byte[] bytes);
    }
}
