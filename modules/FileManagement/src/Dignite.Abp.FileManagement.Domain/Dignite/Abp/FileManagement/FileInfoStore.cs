using Dignite.Abp.BlobStoring;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using System;

namespace Dignite.Abp.FileManagement
{
    public class FileInfoStore: DomainService, IBlobInfoStore
    {
        private readonly IFileRepository _blobRepository;
        private readonly IOptions<FileOptions> _options;
        private readonly ICurrentBlobInfo _currentFile;

        public FileInfoStore(
            IFileRepository blobRepository, 
            IOptions<FileOptions> options,
            ICurrentBlobInfo currentFile)
        {
            _blobRepository = blobRepository;
            _options = options;
            _currentFile= currentFile;
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
            var file = (File)_currentFile.BlobInfo;
            if (!_options.Value.EntityTypes.Any(x => x.EntityType == file.EntityType))
            {
                throw new EntityFileNotAddableException(file.EntityType);
            }
            file.SetBlobInfo(blobInfo);

            var files = await _blobRepository.GetListAsync(file.EntityType,file.EntityId);
            if (files.Any(f => f.ContainerName == file.ContainerName && f.BlobName == file.BlobName))
            {
                if (file.ReferBlobName.IsNullOrEmpty())
                {
                    files[0].BinarySize = file.BinarySize;
                    files[0].FileName = file.FileName;
                    files[0].Hash = file.Hash;
                    await _blobRepository.UpdateAsync(files[0]);
                }
            }
            else
            {
                await _blobRepository.InsertAsync(file, cancellationToken: cancellationToken);
            }
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
