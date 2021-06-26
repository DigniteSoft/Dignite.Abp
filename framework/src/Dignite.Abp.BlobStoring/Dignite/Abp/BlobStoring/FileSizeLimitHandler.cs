using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Processing;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp;

namespace Dignite.Abp.BlobStoring
{
    /// <summary>
    /// Handler for limiting the size of blob
    /// </summary>
    public class FileSizeLimitHandler : IBlobProcessHandler
    {
        public Task ProcessAsync(BlobProcessHandlerContext context)
        {
            var configuration = context.ContainerConfiguration.GetFileSizeLimitConfiguration();
            if (configuration.MaximumFileSize < context.BlobStream.Length)
            {
                throw new BusinessException(
                    code: "Dignite.Abp.BlobStoring:010008",
                    message: "Blob object is too large",
                    details: $"The blob object size cannot exceed {configuration.MaximumFileSize}M!"
                );
            }

            return Task.CompletedTask;
        }
    }
}
