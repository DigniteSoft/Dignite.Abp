using System;
using Volo.Abp.BlobStoring;
using Volo.Abp.Collections;

namespace Dignite.Abp.BlobStoring
{
    public static class ImageResizeHandlerConfigurationExtensions
    {
        public static ImageResizeHandlerConfiguration GetImageResizeConfiguration(
            this BlobContainerConfiguration containerConfiguration)
        {
            return new ImageResizeHandlerConfiguration(containerConfiguration);
        }

        public static void AddImageResizeHandler(
            this BlobContainerConfiguration containerConfiguration,
            Action<ImageResizeHandlerConfiguration> configureAction)
        {
            var imageResizeProcessHandlers = containerConfiguration.GetConfigurationOrDefault(
                DigniteAbpBlobContainerConfigurationNames.BlobProcessHandlers,
                new TypeList<IBlobProcessHandler>());

            if (blobProcessHandlers.TryAdd<ImageResizeHandler>())
            {
                configureAction(new ImageResizeHandlerConfiguration(containerConfiguration));
            }
        }
    }
}
