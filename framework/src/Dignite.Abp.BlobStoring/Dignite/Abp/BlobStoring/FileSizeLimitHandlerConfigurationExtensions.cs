using System;
using Volo.Abp.BlobStoring;
using Volo.Abp.Collections;

namespace Dignite.Abp.BlobStoring
{
    public static class FileSizeLimitHandlerConfigurationExtensions
    {
        public static FileSizeLimitHandlerConfiguration GetFileSizeLimitConfiguration(
            this BlobContainerConfiguration containerConfiguration)
        {
            return new FileSizeLimitHandlerConfiguration(containerConfiguration);
        }

        public static void AddFileSizeLimitHandler(
            this BlobContainerConfiguration containerConfiguration,
            Action<FileSizeLimitHandlerConfiguration> configureAction)
        {
            var blobProcessHandlers = containerConfiguration.GetConfigurationOrDefault(
                DigniteAbpBlobContainerConfigurationNames.BlobProcessHandlers,
                new TypeList<IBlobProcessHandler>());


            if (blobProcessHandlers.TryAdd<FileSizeLimitHandler>())
            {
                configureAction(new FileSizeLimitHandlerConfiguration(containerConfiguration));

                containerConfiguration.SetConfiguration(
                    DigniteAbpBlobContainerConfigurationNames.BlobProcessHandlers,
                    blobProcessHandlers);
            }
        }
    }
}
