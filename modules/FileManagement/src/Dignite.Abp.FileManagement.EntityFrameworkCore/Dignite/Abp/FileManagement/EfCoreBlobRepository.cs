using Dignite.Abp.FileManagement.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Dignite.Abp.FileManagement
{
    public class EfCoreBlobRepository : EfCoreRepository<IFileManagementDbContext, Blob, Guid>, IBlobRepository
    {
        public EfCoreBlobRepository(
            IDbContextProvider<IFileManagementDbContext> dbContextProvider
            )
            : base(dbContextProvider)
        {
        }


        public async Task<bool> ExistsAsync(string containerName, string blobName, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                       .AnyAsync(b => b.ContainerName == containerName && b.BlobName == blobName, GetCancellationToken(cancellationToken));
        }
        public async Task<bool> HashExistsAsync(string containerName, string hash, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                       .AnyAsync(b => b.ContainerName == containerName && b.Hash == hash, GetCancellationToken(cancellationToken));
        }
        public async Task<Blob> FindAsync(string containerName, string blobName, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                       .FirstOrDefaultAsync(b => b.ContainerName == containerName && b.BlobName == blobName, GetCancellationToken(cancellationToken));
        }
        public async Task<Blob> FindByBlobHashAsync(string containerName, string hash, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                       .FirstOrDefaultAsync(b => b.ContainerName == containerName && b.Hash == hash, GetCancellationToken(cancellationToken));
        }

        public async Task<bool> ReferenceExistsAsync(string containerName, string blobName, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                       .AnyAsync(b => b.ContainerName == containerName && b.ReferBlobName == blobName, GetCancellationToken(cancellationToken));
        }

        public async Task<List<Blob>> GetListAsync(string entityType, string entityId, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .Where(b => b.EntityType == entityType && b.EntityId == entityId)
                .ToListAsync(cancellationToken);
        }
    }
}
