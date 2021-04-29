using System;
using Volo.Abp.BlobStoring;
using Volo.Abp.Collections;

namespace Dignite.Abp.BlobStoring
{
    public static class FileTypeCheckHandlerConfigurationExtensions
    {
        public static FileTypeCheckHandlerConfiguration GetFileTypeCheckConfiguration(
            this BlobContainerConfiguration containerConfiguration)
        {
            return new FileTypeCheckHandlerConfiguration(containerConfiguration);
        }

        public static void AddFileTypeCheckHandler(
            this BlobContainerConfiguration containerConfiguration,
            Action<FileTypeCheckHandlerConfiguration> configureAction)
        {
            var blobProcessHandlers = containerConfiguration.GetConfigurationOrDefault(
                DigniteAbpBlobContainerConfigurationNames.BlobProcessHandlers,
                new TypeList<IBlobProcessHandler>());

            if (blobProcessHandlers.TryAdd<FileTypeCheckHandler>())
            {
                configureAction(new FileTypeCheckHandlerConfiguration(containerConfiguration));
            }
        }
    }
}
