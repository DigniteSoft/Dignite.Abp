using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Dignite.Abp.FileManagement
{
    public interface IFileRepository : IBasicRepository<File, Guid>
    {
        Task<bool> ExistsAsync(string containerName, string blobName, CancellationToken cancellationToken = default);
        Task<bool> HashExistsAsync(string containerName, string hash, CancellationToken cancellationToken = default);        
        Task<bool> ReferenceExistsAsync(string containerName, string blobName, CancellationToken cancellationToken = default);
        Task<File> FindAsync(string containerName, string blobName, CancellationToken cancellationToken = default);
        Task<File> FindByBlobHashAsync(string containerName, string hash, CancellationToken cancellationToken = default);

        Task<List<File>> GetListAsync(string entityType, string entityId, CancellationToken cancellationToken = default);
    }
}
