
using System.IO;
using System.Threading.Tasks;

namespace Dignite.Abp.BlobStoring
{
    public interface IBlobProcessHandler
    {
        Task<Stream> ProcessAsync(BlobProcessHandlerContext context);
    }
}
