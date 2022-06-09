using SixLabors.ImageSharp;
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
    public class ImageProcessHandler : IBlobProcessHandler,ITransientDependency
    {
        public async Task<Stream> ProcessAsync(BlobProcessHandlerContext context)
        {
            //Ensure that the starting position of the data flow is 0
            if (context.BlobStream.Position > 0)
            {
                context.BlobStream.Position = 0;
            }
            var configuration = context.ContainerConfiguration.GetImageResizeConfiguration();


            try
            {
                using (Image image = await Image.LoadAsync(context.BlobStream))
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
                        image.Metadata.ExifProfile = null;
                        image.Metadata.XmpProfile = null;
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
                            Quality = 90
                        };

                        context.BlobStream.Position = 0;
                        image.Save(context.BlobStream, encoder);
                    }

                    //Reset the data stream position to 0
                    context.BlobStream.Position = 0;
                    return context.BlobStream;
                }
            }
            catch (SixLabors.ImageSharp.UnknownImageFormatException exception)
            {
                //Reset the data stream position to 0
                context.BlobStream.Position = 0;
                return context.BlobStream;
            }
        }
    }
}
