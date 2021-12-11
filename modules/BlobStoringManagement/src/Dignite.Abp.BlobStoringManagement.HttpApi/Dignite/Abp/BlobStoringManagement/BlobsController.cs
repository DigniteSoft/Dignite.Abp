using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using HeyRed.Mime;

namespace Dignite.Abp.BlobStoringManagement
{
    [Area("BlobStoringManagement")]
    [RemoteService(Name = BlobStoringManagementRemoteServiceConsts.RemoteServiceName)]
    [Route("api/blob-storing/blobs")]
    public class BlobsController : AbpController, IBlobsAppService
    {
        private readonly IBlobsAppService _blobAppService;

        public BlobsController(
            IBlobsAppService blobAppService
            )
        {
            _blobAppService = blobAppService;
        }


        [HttpPost]
        [Route("save-remote-file/{containerName}")]
        public async Task<BlobDto> SaveRemoteFileAsync([NotNull] string containerName, SaveRemoteFileInput input)
        {
            return await _blobAppService.SaveRemoteFileAsync(containerName, input);
        }


        [HttpPost]
        [Route("save/{containerName}")]
        public async Task<BlobDto> SaveAsync([NotNull] string containerName, SaveBytesInput input)
        {
            return await _blobAppService.SaveAsync(containerName, input);
        }


        [HttpPost]
        [Route("upload/{containerName}")]
        public async Task<BlobDto> UploadAsync([NotNull] string containerName, IFormFile File, string EntityType, string EntityId)
        {
            return await _blobAppService.SaveAsync(containerName,
                new SaveBytesInput
                {
                    Bytes = File.GetAllBytes(),
                    EntityType = EntityType,
                    EntityId = EntityId,
                    FileName = File.FileName
                });
        }

        [HttpGet]
        [Route("{containerName}/configuration")]
        public Task<BlobContainerConfigurationDto> GetBlobContainerConfigurationAsync([NotNull] string containerName)
        {
            return _blobAppService.GetBlobContainerConfigurationAsync(containerName);
        }

        [HttpGet]
        [Route("{containerName}/{blobName}")]
        public async Task<FileResult> LoadAsync([NotNull] string containerName, [NotNull] string blobName)
        {
            var stream = await GetOrNullAsync(containerName, blobName);
            var mimeType = MimeTypesMap.GetMimeType(blobName);
            return File(stream, mimeType);
        }

        [HttpPost]
        [Route("download/{containerName}/{blobName}")]
        public async Task<FileResult> DownloadAsync([NotNull] string containerName, [NotNull] string blobName, [NotNull] string fileName)
        {
            var stream = await GetOrNullAsync(containerName, blobName);
            stream.Position = 0;
            var mimeType = MimeTypesMap.GetMimeType(blobName);
            return File(stream, mimeType, fileName);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="entityId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ListResultDto<BlobDto>> GetListAsync(string entityType, string entityId)
        {
            return await _blobAppService.GetListAsync(entityType, entityId);
        }


        [HttpDelete]
        [Route("{containerName}")]
        public async Task DeleteAsync([NotNull] string containerName, [NotNull] string blobName)
        {
            await _blobAppService.DeleteAsync(containerName, blobName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="entityId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete-by-entity")]
        public async Task DeleteByEntityAsync(string entityType, string entityId)
        {
            await _blobAppService.DeleteByEntityAsync(entityType, entityId);
        }


        [RemoteService(IsEnabled = false)]
        public async Task<Stream> GetOrNullAsync([NotNull] string containerName, [NotNull] string blobName)
        {
            return await _blobAppService.GetOrNullAsync(containerName, blobName);
        }
    }
}
