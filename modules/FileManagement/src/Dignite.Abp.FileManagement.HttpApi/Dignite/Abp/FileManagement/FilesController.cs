using HeyRed.Mime;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Content;

namespace Dignite.Abp.FileManagement
{
    [Area("FileManagement")]
    [RemoteService(Name = FileManagementRemoteServiceConsts.RemoteServiceName)]
    [Route("api/file-management/files")]
    public class FilesController : AbpController, IFilesAppService
    {
        private readonly IFilesAppService _blobAppService;

        public FilesController(
            IFilesAppService blobAppService
            )
        {
            _blobAppService = blobAppService;
        }


        [HttpPost]
        [Route("save/{containerName}/remote-file")]
        public async Task<FileDto> SaveAsync([NotNull] string containerName, SaveRemoteFileInput input)
        {
            return await _blobAppService.SaveAsync(containerName, input);
        }

        [HttpPost]
        [Route("save/{containerName}")]
        public async Task<FileDto> SaveAsync([NotNull] string containerName, SaveStreamInput input)
        {
            return await _blobAppService.SaveAsync(containerName, input);
        }



        [HttpGet]
        [Route("{containerName}/configuration")]
        public Task<BlobContainerConfigurationDto> GetBlobContainerConfigurationAsync([NotNull] string containerName)
        {
            return _blobAppService.GetBlobContainerConfigurationAsync(containerName);
        }

        

        [HttpPost]
        [Route("{containerName}/{blobName}/download")]
        public async Task<FileResult> DownloadAsync([NotNull] string containerName, [NotNull] string blobName, [NotNull] string fileName)
        {
            var file= await _blobAppService.GetFileAsync(containerName, blobName);
            var mimeType = MimeTypesMap.GetMimeType(fileName);
            return File(file.GetStream(), mimeType, fileName);
        }

        [HttpGet]
        [Route("{containerName}/{blobName}")]
        public async Task<IRemoteStreamContent> GetFileAsync([NotNull] string containerName, [NotNull] string blobName)
        {
            return await _blobAppService.GetFileAsync(containerName, blobName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="entityId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ListResultDto<FileDto>> GetListAsync(string entityType, string entityId)
        {
            return await _blobAppService.GetListAsync(entityType, entityId);
        }


        [HttpDelete]
        [Route("{containerName}/{blobName}")]
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
    }
}
