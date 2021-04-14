using System.Threading.Tasks;

namespace Dignite.Abp.BlobStoring
{
    public interface IBlobInfoStore
    {
        Task<bool> AnyAsync(string containerName, string blobName);

        Task<bool> AnyByHashAsync(string containerName,string hash);

        Task<IBlobInfo> GetMainAsync(string containerName, string hash);

        Task CreateAsync(IBlobInfo blobInfo);
    }
}
