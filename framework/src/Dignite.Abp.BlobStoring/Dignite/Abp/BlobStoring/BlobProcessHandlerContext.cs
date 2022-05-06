using System;
using System.IO;
using Volo.Abp.BlobStoring;

namespace Dignite.Abp.BlobStoring
{
    public class BlobProcessHandlerContext
    {
        public BlobProcessHandlerContext(
            Stream blobStream,
            BlobContainerConfiguration containerConfiguration,
            IServiceProvider serviceProvider
            )
        {
            BlobStream             = blobStream;
            ContainerConfiguration = containerConfiguration;
            ServiceProvider        = serviceProvider;
        }



        public IServiceProvider ServiceProvider { get; }

        public Stream BlobStream { get; set; }

        public BlobContainerConfiguration ContainerConfiguration { get; }
    }
}
