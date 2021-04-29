using JetBrains.Annotations;
using System.Collections.Generic;

namespace Dignite.Abp.BlobStoring
{
    public class BasicBlobInfo : IBlobInfo
    {
        public BasicBlobInfo(
            [NotNull] string containerName,
            [NotNull] string blobName
            )
        {
            ContainerName = containerName;
            BlobName      = blobName;
        }


        [NotNull]
        public string ContainerName { get; private set; }


        [NotNull]
        public string BlobName { get; private set; }

        [NotNull]
        public long BinarySize { get; set; }

        [CanBeNull]
        public string Hash { get; set; }

        [CanBeNull]
        public string ReferBlobName { get; set; }

        [CanBeNull]
        public Dictionary<string, object> Properties { get; private set; }

    }
}
