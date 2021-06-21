using Dignite.Abp.BlobStoringManagement.MongoDB;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;

namespace Dignite.Abp.BlobStoringManagement
{
    public class MongoBlobRepository : MongoDbRepository<IBlobStoringManagementMongoDbContext, Blob, Guid>, IBlobRepository
    {
        public MongoBlobRepository(IMongoDbContextProvider<IBlobStoringManagementMongoDbContext> dbContextProvider) 
            : base(dbContextProvider)
        {
        }

        public async Task<bool> ExistsAsync(string containerName, string blobName, CancellationToken cancellationToken = default)
        {
            var token = GetCancellationToken(cancellationToken);
            return await (await GetMongoQueryableAsync(token))
                       .AnyAsync(b => b.ContainerName == containerName && b.BlobName == blobName, token);
        }
        public async Task<bool> HashExistsAsync(string containerName, string hash, CancellationToken cancellationToken = default)
        {
            var token = GetCancellationToken(cancellationToken);
            return await (await GetMongoQueryableAsync(token))
                       .AnyAsync(b => b.ContainerName == containerName && b.Hash == hash, token);
        }
        public async Task<Blob> FindAsync(string containerName, string blobName, CancellationToken cancellationToken = default)
        {
            var token = GetCancellationToken(cancellationToken);
            return await (await GetMongoQueryableAsync(token))
                       .FirstOrDefaultAsync(b => b.ContainerName == containerName && b.BlobName == blobName, token);
        }
        public async Task<Blob> FindByBlobHashAsync(string containerName, string hash, CancellationToken cancellationToken = default)
        {
            var token = GetCancellationToken(cancellationToken);
            return await (await GetMongoQueryableAsync(token))
                       .FirstOrDefaultAsync(b => b.ContainerName == containerName && b.Hash == hash, token);
        }

        public async Task<bool> ReferenceExistsAsync(string containerName, string blobName, CancellationToken cancellationToken = default)
        {
            var token = GetCancellationToken(cancellationToken);
            return await (await GetMongoQueryableAsync(token))
                       .AnyAsync(b => b.ContainerName == containerName && b.ReferBlobName == blobName, token);
        }

        public async Task<List<Blob>> GetListAsync(string entityType, string entityId, CancellationToken cancellationToken = default)
        {
            var token = GetCancellationToken(cancellationToken);
            return await (await GetMongoQueryableAsync(token))
                .Where(b => b.EntityType == entityType && b.EntityId == entityId)
                .ToListAsync(token);
        }
    }
}
