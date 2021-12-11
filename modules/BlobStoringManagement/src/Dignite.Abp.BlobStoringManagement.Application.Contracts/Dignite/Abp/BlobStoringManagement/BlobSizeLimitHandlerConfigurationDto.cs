

namespace Dignite.Abp.BlobStoringManagement
{
    public class BlobSizeLimitHandlerConfigurationDto
    {
        public BlobSizeLimitHandlerConfigurationDto(int maximumBlobSize)
        {
            MaximumBlobSize = maximumBlobSize;
        }

        /// <summary>
        /// Limit file size(MB)
        /// </summary>
        public int MaximumBlobSize { get; set; }
    }
}
