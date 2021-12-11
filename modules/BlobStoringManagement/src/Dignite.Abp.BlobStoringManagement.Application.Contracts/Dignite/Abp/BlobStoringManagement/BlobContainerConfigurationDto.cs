namespace Dignite.Abp.BlobStoringManagement
{
    public class BlobContainerConfigurationDto
    {
        public BlobContainerConfigurationDto(AuthorizationHandlerConfigurationDto authorizationHandlerConfiguration, BlobSizeLimitHandlerConfigurationDto blobSizeLimitHandlerConfiguration, FileTypeCheckHandlerConfigurationDto fileTypeCheckHandlerConfiguration, ImageProcessHandlerConfigurationDto imageProcessHandlerConfiguration)
        {
            AuthorizationHandlerConfiguration = authorizationHandlerConfiguration;
            BlobSizeLimitHandlerConfiguration = blobSizeLimitHandlerConfiguration;
            FileTypeCheckHandlerConfiguration = fileTypeCheckHandlerConfiguration;
            ImageProcessHandlerConfiguration = imageProcessHandlerConfiguration;
        }

        public AuthorizationHandlerConfigurationDto AuthorizationHandlerConfiguration{ get; }
        public BlobSizeLimitHandlerConfigurationDto BlobSizeLimitHandlerConfiguration { get; }
        public FileTypeCheckHandlerConfigurationDto FileTypeCheckHandlerConfiguration { get; }
        public ImageProcessHandlerConfigurationDto ImageProcessHandlerConfiguration { get; }
    }
}
