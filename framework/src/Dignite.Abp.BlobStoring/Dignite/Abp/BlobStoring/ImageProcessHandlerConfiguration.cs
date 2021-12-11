using Volo.Abp.BlobStoring;

namespace Dignite.Abp.BlobStoring
{
    public class ImageProcessHandlerConfiguration
    {
        /// <summary>
        /// Scale the width of the image
        /// </summary>
        public int ImageWidth
        {
            get => _containerConfiguration.GetConfigurationOrDefault<int>(ImageProcessHandlerConfigurationNames.ImageWidth);
            set => _containerConfiguration.SetConfiguration(ImageProcessHandlerConfigurationNames.ImageWidth, value);
        }

        /// <summary>
        /// Scale the height of the image
        /// </summary>
        public int ImageHeight
        {
            get => _containerConfiguration.GetConfigurationOrDefault<int>(ImageProcessHandlerConfigurationNames.ImageHeight);
            set => _containerConfiguration.SetConfiguration(ImageProcessHandlerConfigurationNames.ImageHeight, value);
        }

        /// <summary>
        /// Whether allow uploaded image's size larger than preset size
        /// </summary>
        public bool ImageSizeMustBeLargerThanPreset
        {
            get => _containerConfiguration.GetConfigurationOrDefault<bool>(ImageProcessHandlerConfigurationNames.ImageSizeMustBeLargerThanPreset, false);
            set => _containerConfiguration.SetConfiguration(ImageProcessHandlerConfigurationNames.ImageHeight, value);
        }

        private readonly BlobContainerConfiguration _containerConfiguration;

        public ImageProcessHandlerConfiguration(BlobContainerConfiguration containerConfiguration)
        {
            _containerConfiguration = containerConfiguration;
        }
    }
}
