using System.Threading;
using System.Threading.Tasks;

namespace Dignite.Abp.BlobStoring
{
    public interface IBlobInfoStore
    {
        Task<bool> ExistsAsync(string containerName, string blobName, CancellationToken cancellationToken = default);

        Task<IBlobInfo> FindAsync(string containerName, string blobName, CancellationToken cancellationToken = default);

        Task<bool> HashExistsAsync(string containerName,string hash, CancellationToken cancellationToken = default);

        Task<IBlobInfo> FindByHashAsync(string containerName, string hash, CancellationToken cancellationToken = default);

        /// <summary>
        /// Query if there is a blob referenced by another blob
        /// </summary>
        /// <param name="containerName"></param>
        /// <param name="blobName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> ReferenceExistsAsync(string containerName, string blobName, CancellationToken cancellationToken = default);

        Task CreateAsync(BasicBlobInfo blobInfo, CancellationToken cancellationToken = default);

        Task DeleteAsync(string containerName, string blobName, CancellationToken cancellationToken = default);
    }
}
