using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.BlobStoring
{
    /// <summary>
    /// Resize uploaded images to fit predefined values
    /// </summary>
    public class ImageProcessHandler : IBlobProcessHandler,ITransientDependency
    {
        public Task ProcessAsync(BlobProcessHandlerContext context)
        {
            var configuration = context.ContainerConfiguration.GetImageResizeConfiguration();

            try
            {
                using (Image image = Image.Load(context.BlobStream))
                {
                    if (configuration.ImageSizeMustBeLargerThanPreset)
                    {
                        if (image.Width < configuration.ImageWidth || image.Height < configuration.ImageHeight)
                        {
                            throw new BusinessException(
                                code: "Dignite.Abp.BlobStoring:010004",
                                message: "Image size must be larger than Preset!",
                                details: "Uploaded image must be larger than: " + configuration.ImageWidth + "x" + configuration.ImageHeight
                            );
                        }
                    }


                    if (image.Width > configuration.ImageWidth || image.Height > configuration.ImageHeight)
                    {
                        image.Mutate(x =>
                        {
                            x.Resize(new ResizeOptions()
                            {
                                Mode = ResizeMode.Max,
                                Size = new Size(configuration.ImageWidth, configuration.ImageHeight)
                            });
                        });

                        var encoder = new SixLabors.ImageSharp.Formats.Jpeg.JpegEncoder()
                        {
                            Quality = 40
                        };
                        context.BlobStream.Position = 0;
                        image.Save(context.BlobStream, encoder);
                        context.BlobStream.SetLength(context.BlobStream.Position);
                        context.BlobStream.Position = 0;
                    }
                }
            }
            catch (SixLabors.ImageSharp.UnknownImageFormatException exception)
            {
            }
            return Task.CompletedTask;
            
        }
    }
}
