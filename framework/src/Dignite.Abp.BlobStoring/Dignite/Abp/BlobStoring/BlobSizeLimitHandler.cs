using System.IO;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.BlobStoring
{
    /// <summary>
    /// Handler for limiting the size of blob
    /// </summary>
    public class BlobSizeLimitHandler : IBlobProcessHandler, ITransientDependency
    {
        public async Task<Stream> ProcessAsync(BlobProcessHandlerContext context)
        {
            var configuration = context.ContainerConfiguration.GetBlobSizeLimitConfiguration();
            if (configuration.MaximumBlobSize*1024*1024 < context.BlobStream.Length)
            {
                throw new BusinessException(
                    code: "Dignite.Abp.BlobStoring:010008",
                    message: "Blob object is too large",
                    details: $"The blob object size cannot exceed {configuration.MaximumBlobSize}M!"
                );
            }

            return context.BlobStream;
        }
    }
}
