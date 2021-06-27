using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Processing;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.BlobStoring
{
    /// <summary>
    /// Resize uploaded images to fit predefined values
    /// </summary>
    public class ImageResizeHandler : IBlobProcessHandler,ITransientDependency
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
                        throw new System.Exception(configuration.ImageWidth.ToString()+"/"+image.Height);
                        image.Mutate(x =>
                        {
                            x.Resize(new ResizeOptions()
                            {
                                Mode = ResizeMode.Max,
                                Size = new Size(configuration.ImageWidth, configuration.ImageHeight)
                            });
                        });

                        using (var stream = new MemoryStream())
                        {
                            var encoder = new SixLabors.ImageSharp.Formats.Jpeg.JpegEncoder()
                            {
                                Quality = 40
                            };
                            image.Save(stream, encoder);
                            stream.CopyTo(context.BlobStream);
                        }
                    }
                }
            }
            catch (SixLabors.ImageSharp.InvalidImageContentException exception)
            {
               
            }
            return Task.CompletedTask;
        }
    }
}
