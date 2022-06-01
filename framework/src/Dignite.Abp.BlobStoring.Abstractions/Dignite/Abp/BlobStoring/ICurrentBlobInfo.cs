using System;

namespace Dignite.Abp.BlobStoring
{
    public interface ICurrentBlobInfo
    {
        bool IsAvailable { get; }

        IBlobInfo BlobInfo { get; }

        IDisposable Current(IBlobInfo blobInfo);
    }
}
