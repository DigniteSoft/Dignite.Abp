using System.Threading.Tasks;

namespace Dignite.Abp.BlobStoring
{
    public class NullSavedCallback : ISavedCallback
    {
        public Task CallbackAsync(string blobName)
        {
            return Task.CompletedTask;
        }
    }
}
