using Volo.Abp.BlobStoring;

namespace Dignite.Abp.BlobStoring
{
    public class ImageResizeHandlerConfiguration
    {
        /// <summary>
        /// Scale the width of the image
        /// </summary>
        public int ImageWidth
        {
            get => _containerConfiguration.GetConfigurationOrDefault<float?>(ImageResizeHandlerConfigurationNames.ImageWidth, null);
            set => _containerConfiguration.SetConfiguration(ImageResizeHandlerConfigurationNames.ImageWidth, value);
        }

        /// <summary>
        /// Scale the height of the image
        /// </summary>
        public int ImageHeight
        {
            get => _containerConfiguration.GetConfigurationOrDefault<float?>(ImageResizeHandlerConfigurationNames.ImageHeight, null);
            set => _containerConfiguration.SetConfiguration(ImageResizeHandlerConfigurationNames.ImageHeight, value);
        }

        /// <summary>
        /// Whether allow uploaded image's size less than preset size
        /// </summary>
        public bool ImageSizeCouldBeLessThanPreset
        {
            get => _containerConfiguration.GetConfigurationOrDefault<bool>(ImageResizeHandlerConfigurationNames.SizeCouldBeLessThanPreset, true);
            set => _containerConfiguration.SetConfiguration(ImageResizeHandlerConfigurationNames.ImageHeight, value);
        }

        private readonly BlobContainerConfiguration _containerConfiguration;

        public ImageResizeHandlerConfiguration(BlobContainerConfiguration containerConfiguration)
        {
            _containerConfiguration = containerConfiguration;
        }
    }
}
