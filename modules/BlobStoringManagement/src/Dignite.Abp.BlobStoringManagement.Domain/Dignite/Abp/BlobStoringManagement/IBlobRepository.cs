using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Dignite.Abp.BlobStoringManagement
{
    public interface IBlobRepository : IBasicRepository<Blob, Guid>
    {
        Task<bool> ExistsAsync(string containerName, string blobName, CancellationToken cancellationToken = default);
        Task<bool> HashExistsAsync(string containerName, string hash, CancellationToken cancellationToken = default);        
        Task<bool> ReferenceExistsAsync(string containerName, string blobName, CancellationToken cancellationToken = default);
        Task<Blob> FindAsync(string containerName, string blobName, CancellationToken cancellationToken = default);
        Task<Blob> FindByBlobHashAsync(string containerName, string hash, CancellationToken cancellationToken = default);

        Task<List<Blob>> GetListAsync(string entityType, string entityId, CancellationToken cancellationToken = default);
    }
}
