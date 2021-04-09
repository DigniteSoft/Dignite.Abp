using System.Collections.Generic;

namespace Dignite.Abp.BlobStoring
{
    public interface IBlobInfo
    {
        string ContainerName { get; }

        string BlobName { get; }

        long BinarySize { get; }

        string Hash { get; }

        /// <summary>
        /// 引用的Blob
        /// </summary>
        string ReferBlobName { get; }


        Dictionary<string, object> Properties { get; }
    }
}
