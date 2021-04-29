using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;

namespace Dignite.Abp.BlobStoring
{
    /// <summary>
    /// 对上传的图片文件进行缩放处理
    /// </summary>
    public class ImageResizeHandler : IBlobProcessHandler
    {
        public Task ProcessAsync(BlobProcessHandlerContext context)
        {
            using SixLabors.ImageSharp;

            var ImageResizeHandlerConfiguration = context.ContainerConfiguration.GetImageResizeConfiguration();
            Image Img = Image.Load(context.BlobStream);

            private void ImageResizeScaled(IImageProcessingContext image)
            {
                using SixLabors.ImageSharp.Processing;

                if (image.Height > imageResize.ImageHeight && image.Width > imageResize.ImageWidth)
                {
                    if ((image.Height - imageResize.ImageHeight) > (image.Width - imageResize.ImageWidth))
                    {
                        image.Width  = (imageResize.ImageHeight / image.Height) * image.Width;
                        image.Height = imageResize.ImageHeight;
                    }
                    else if ((image.Height - imageResize.ImageHeight) == (image.Width - imageResize.ImageWidth))
                    {
                        if (imageResize.ImageHeight < imageResize.ImageWidth)
                        {
                            image.Width  = imageResize.ImageWidth;
                            image.Height = (imageResize.ImageWidth / image.Width) * image.Height;
                        }
                        else if (imageResize.ImageHeight > imageResize.ImageWidth)
                        {
                            image.Width  = (imageResize.ImageHeight / image.Height) * image.Width;
                            image.Height = imageResize.ImageHeight;
                        }
                    }
                    else
                    {
                        image.Width  = imageResize.ImageWidth;
                        image.Height = (imageResize.ImageWidth / image.Width) * image.Height;
                    }
                }
                else if (image.Height > imageResize.ImageHeight && image.Width < imageResize.ImageWidth)
                {
                    image.Width  = (imageResize.ImageHeight / image.Height) * image.Width;
                    image.Height = imageResize.ImageHeight;
                }
                else if (image.Height < imageResize.ImageHeight && image.Width > imageResize.ImageWidth)
                {
                    image.Width  = imageResize.ImageWidth;
                    image.Height = (imageResize.ImageWidth / image.Width) * image.Height;
                }

                image.Mutate(x => x.Resize(image.Width, image.Height));
                // Automatic encoder selected based on extension
                image.Save(context.BlobStream);
            }

            /// <summary>
            /// When not allowed less than preset
            /// if less, throw a new exception
            /// else scale it to the preset value
            /// When allowed(DEFAULT) less than preset
            /// scale it to the preset value
            /// </summary>
            if (imageResize.ImageHeight != null && imageResize.ImageWidth != null)
            {
                if (ImageResizeHandlerConfiguration.ImageSizeCouldBeLessThanPreset == false)
                {
                    if (Img.Width >= imageResize.ImageWidth && Img.Height >= imageResize.ImageHeight)
                    {
                        ImageResizeScaled(Img);
                    }
                    else
                    {
                        throw new BusinessException(
                            code: "Dignite.Abp.BlobStoring:010004",
                            message: "Image size should not be less than required!",
                            details: "Uploaded image should not be less than: " + imageResize.ImageWidth + "x" + imageResize.ImageHeight
                        );
                    }
                }
                else
                {
                    ImageResizeScaled(Img);
                }
            }

            return Task.CompletedTask;
        }

    }
}
