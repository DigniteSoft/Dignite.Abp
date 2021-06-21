
using System.Threading.Tasks;

namespace Dignite.Abp.BlobStoring
{
    public interface IBlobProcessHandler
    {
        Task ProcessAsync(BlobProcessHandlerContext context);
    }
}
