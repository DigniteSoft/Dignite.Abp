using System;
using System.IO;
using Volo.Abp.BlobStoring;
using Volo.Abp.MultiTenancy;

namespace Dignite.Abp.BlobStoring
{
    public class BlobProcessHandlerContext
    {
        public BlobProcessHandlerContext(
            Stream blobStream,
            BlobContainerConfiguration containerConfiguration,
            ICurrentTenant currentTenant,
            IServiceProvider serviceProvider
            )
        {
            BlobStream             = blobStream;
            ContainerConfiguration = containerConfiguration;
            CurrentTenant          = currentTenant;
            ServiceProvider        = serviceProvider;
        }


        public ICurrentTenant CurrentTenant { get; }

        public IServiceProvider ServiceProvider { get; }

        public Stream BlobStream { get; set; }

        public BlobContainerConfiguration ContainerConfiguration { get; }
    }
}
