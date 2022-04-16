using Dignite.Abp.BlobStoring;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Dignite.Abp.FileManagement
{
    public class FileInfoStore: DomainService, IBlobInfoStore
    {
        private readonly IFileRepository _blobRepository;
        private readonly IFileEntityResolver _blobEntityResolver;
        private readonly IOptions<FileOptions> _options;

        public FileInfoStore(IFileRepository blobRepository, IFileEntityResolver blobEntityResolver,
            IOptions<FileOptions> options)
        {
            _blobRepository = blobRepository;
            _blobEntityResolver = blobEntityResolver;
            _options = options;
        }

        public async Task<bool> ExistsAsync(string containerName, string blobName, CancellationToken cancellationToken = default)
        {
            return await _blobRepository.ExistsAsync(containerName, blobName, cancellationToken);
        }

        public async Task<bool> HashExistsAsync(string containerName, string hash, CancellationToken cancellationToken = default)
        {
            return await _blobRepository.HashExistsAsync(containerName, hash, cancellationToken);
        }

        public async Task CreateAsync(IBlobInfo blobInfo, CancellationToken cancellationToken = default)
        {
            var blobEntityResult = await _blobEntityResolver.ResolveBlobEntityAsync();

            Check.NotNullOrWhiteSpace(blobEntityResult.EntityType, nameof(blobEntityResult.EntityType), FileConsts.MaxEntityTypeLength);
            Check.NotNullOrWhiteSpace(blobEntityResult.EntityId, nameof(blobEntityResult.EntityId), FileConsts.MaxEntityIdLength);
            Check.NotNullOrWhiteSpace(blobInfo.ContainerName, nameof(blobInfo.ContainerName), FileConsts.MaxContainerNameLength);
            Check.NotNullOrWhiteSpace(blobInfo.BlobName, nameof(blobInfo.BlobName), FileConsts.MaxBlobNameLength);

            if (!_options.Value.EntityTypes.Any(x => x.EntityType == blobEntityResult.EntityType))
            {
                throw new EntityFileNotAddableException(blobEntityResult.EntityType);
            }

            var blob = new File(
                GuidGenerator.Create(),
                blobEntityResult.EntityType,
                blobEntityResult.EntityId,
                new BasicBlobInfo(blobInfo.ContainerName,blobInfo.BlobName,blobInfo.BinarySize,blobInfo.Hash,blobInfo.ReferBlobName),
                CurrentTenant.Id);

            await _blobRepository.InsertAsync(blob, cancellationToken: cancellationToken);
        }

        public async Task DeleteAsync(string containerName, string blobName, CancellationToken cancellationToken = default)
        {
            var blob = await _blobRepository.FindAsync(containerName, blobName, cancellationToken);
            if (blob != null)
                await _blobRepository.DeleteAsync(blob.Id, cancellationToken:cancellationToken);
        }

        public async Task<IBlobInfo> FindAsync(string containerName, string blobName, CancellationToken cancellationToken = default)
        {
            return await _blobRepository.FindAsync(containerName, blobName, cancellationToken);
        }

        public async Task<IBlobInfo> FindByHashAsync(string containerName, string hash, CancellationToken cancellationToken = default)
        {
            return await _blobRepository.FindByBlobHashAsync(containerName, hash, cancellationToken);
        }        

        public async Task<bool> ReferenceExistsAsync(string containerName, string blobName,  CancellationToken cancellationToken = default)
        {
            return await _blobRepository.ReferenceExistsAsync(containerName, blobName, cancellationToken);
        }
    }
}
