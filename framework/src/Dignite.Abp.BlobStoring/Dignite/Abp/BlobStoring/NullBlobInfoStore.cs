using System.Threading;
using System.Threading.Tasks;

namespace Dignite.Abp.BlobStoring
{
    public class NullBlobInfoStore:IBlobInfoStore
    {
        public NullBlobInfoStore()
        {
        }

        public Task<bool> ExistsAsync(string containerName, string blobName, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(false);
        }

        public async Task<IBlobInfo> FindAsync(string containerName, string blobName, CancellationToken cancellationToken = default)
        {
            BasicBlobInfo blobInfo = null;
            return await Task.FromResult(blobInfo);
        }

        public Task<bool> HashExistsAsync(string containerName, string hash, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(false);
        }
        public async Task<IBlobInfo> FindByHashAsync(string containerName, string hash, CancellationToken cancellationToken = default)
        {
            BasicBlobInfo blobInfo = null;
            return await Task.FromResult(blobInfo);
        }


        public Task<bool> ReferenceExistsAsync(string containerName, string blobName,  CancellationToken cancellationToken = default)
        {
            return Task.FromResult(false);
        }


        public Task CreateAsync(BasicBlobInfo blobInfo, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task DeleteAsync(string containerName, string blobName, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}
