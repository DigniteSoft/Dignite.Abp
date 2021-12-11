using System;
using Volo.Abp.BlobStoring;
using Volo.Abp.Collections;

namespace Dignite.Abp.BlobStoring
{
    public static class ImageProcessHandlerConfigurationExtensions
    {
        public static ImageProcessHandlerConfiguration GetImageResizeConfiguration(
            this BlobContainerConfiguration containerConfiguration)
        {
            return new ImageProcessHandlerConfiguration(containerConfiguration);
        }

        public static void AddImageResizeHandler(
            this BlobContainerConfiguration containerConfiguration,
            Action<ImageProcessHandlerConfiguration> configureAction)
        {
            var blobProcessHandlers = containerConfiguration.GetConfigurationOrDefault(
                DigniteAbpBlobContainerConfigurationNames.BlobProcessHandlers,
                new TypeList<IBlobProcessHandler>());

            if (blobProcessHandlers.TryAdd<ImageProcessHandler>())
            {
                configureAction(new ImageProcessHandlerConfiguration(containerConfiguration));

                containerConfiguration.SetConfiguration(
                    DigniteAbpBlobContainerConfigurationNames.BlobProcessHandlers,
                    blobProcessHandlers);
            }
        }
    }
}
