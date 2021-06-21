using System.Collections.Generic;

namespace Dignite.Abp.BlobStoring
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBlobInfo
    {
        string ContainerName { get; }

        string BlobName { get; }

        /// <summary>
        /// blob binary size
        /// </summary>
        /// <remarks>
        /// If the reference is other <see cref="BasicBlobInfo"/>, the value is 0
        /// </remarks>
        long BinarySize { get; }

        /// <summary>
        /// Hash of blob
        /// </summary>
        /// <remarks>
        /// If the reference is other <see cref="BasicBlobInfo"/>, the value is null
        /// </remarks>
        string Hash { get; }

        /// <summary>
        /// Referencing other blob
        /// </summary>
        string ReferBlobName { get; }
    }
}
