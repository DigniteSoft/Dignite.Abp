using System.Threading.Tasks;

namespace Dignite.Abp.BlobStoring
{
    public interface ISavedCallback
    {
        Task CallbackAsync(string blobName);
    }
}
