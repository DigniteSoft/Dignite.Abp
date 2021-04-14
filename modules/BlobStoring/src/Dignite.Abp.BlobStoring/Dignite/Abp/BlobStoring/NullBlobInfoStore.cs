using System.Threading.Tasks;

namespace Dignite.Abp.BlobStoring
{
    public class NullBlobInfoStore:IBlobInfoStore
    {
        public NullBlobInfoStore()
        {
        }

        public Task<bool> AnyAsync(string containerName, string blobName)
        {
            return Task.FromResult(false);
        }

        public Task<bool> AnyByHashAsync(string containerName, string hash)
        {
            return Task.FromResult(false);
        }
        public async Task<IBlobInfo> GetMainAsync(string containerName, string hash)
        {
            BasicBlobInfo blobInfo = null;
            return await Task.FromResult(blobInfo);
        }

        public Task CreateAsync(IBlobInfo blobInfo)
        {
            return Task.CompletedTask;
        }
    }
}
