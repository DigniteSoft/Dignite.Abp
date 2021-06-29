using Volo.Abp.BlobStoring;

namespace Dignite.Abp.BlobStoring
{
    public class FileSizeLimitHandlerConfiguration
    {

        /// <summary>
        /// Limit file size(MB)
        /// </summary>
        public int MaximumFileSize
        {
            get => _containerConfiguration.GetConfigurationOrDefault<int>(FileSizeLimitHandlerConfigurationNames.MaximumFileSize);
            set => _containerConfiguration.SetConfiguration(FileSizeLimitHandlerConfigurationNames.MaximumFileSize, value);
        }

        private readonly BlobContainerConfiguration _containerConfiguration;

        public FileSizeLimitHandlerConfiguration(BlobContainerConfiguration containerConfiguration)
        {
            _containerConfiguration = containerConfiguration;
        }
    }
}
