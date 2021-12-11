using System;
using Volo.Abp.BlobStoring;
using Volo.Abp.Collections;

namespace Dignite.Abp.BlobStoring
{
    public static class BlobSizeLimitHandlerConfigurationExtensions
    {
        public static BlobSizeLimitHandlerConfiguration GetBlobSizeLimitConfiguration(
            this BlobContainerConfiguration containerConfiguration)
        {
            return new BlobSizeLimitHandlerConfiguration(containerConfiguration);
        }

        public static void AddBlobSizeLimitHandler(
            this BlobContainerConfiguration containerConfiguration,
            Action<BlobSizeLimitHandlerConfiguration> configureAction)
        {
            var blobProcessHandlers = containerConfiguration.GetConfigurationOrDefault(
                DigniteAbpBlobContainerConfigurationNames.BlobProcessHandlers,
                new TypeList<IBlobProcessHandler>());


            if (blobProcessHandlers.TryAdd<BlobSizeLimitHandler>())
            {
                configureAction(new BlobSizeLimitHandlerConfiguration(containerConfiguration));

                containerConfiguration.SetConfiguration(
                    DigniteAbpBlobContainerConfigurationNames.BlobProcessHandlers,
                    blobProcessHandlers);
            }
        }
    }
}
