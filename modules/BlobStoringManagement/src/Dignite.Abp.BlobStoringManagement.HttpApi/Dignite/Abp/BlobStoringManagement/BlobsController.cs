using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.BlobStoring;

namespace Dignite.Abp.BlobStoringManagement
{
    [Area("BlobStoringManagement")]
    [RemoteService(Name = BlobStoringManagementRemoteServiceConsts.RemoteServiceName)]
    [Route("api/blob-storing/blobs")]
    public class BlobsController : AbpController, IBlobsAppService
    {
        private readonly IBlobContainerFactory _blobContainerFactory;
        private readonly IBlobsAppService _blobAppService;

        public BlobsController(
            IBlobContainerFactory blobContainerFactory,
            IBlobsAppService blobAppService
            )
        {
            _blobContainerFactory = blobContainerFactory;
            _blobAppService = blobAppService;
        }


        [HttpPost]
        [Route("save-remote-file/{containerName}")]
        public async Task<string> SaveRemoteFileAsync([NotNull] string containerName, SaveRemoteFileInput input)
        {
            return await _blobAppService.SaveRemoteFileAsync(containerName, input);
        }


        [HttpPost]
        [Route("save/{containerName}")]
        public async Task<string> SaveAsync([NotNull] string containerName, SaveBytesInput input)
        {
            return await _blobAppService.SaveAsync(containerName, input);
        }


        [HttpPost]
        [Route("upload/{containerName}")]
        public async Task<string> UploadAsync([NotNull] string containerName, UploadFileInput input)
        {
            return await _blobAppService.SaveAsync(containerName, 
                new SaveBytesInput
                {
                    Bytes = input.File.GetAllBytes(),
                    EntityType = input.EntityType,
                    EntityId = input.EntityId
                });
        }

        [HttpGet]
        [Route("{containerName}/{blobName}")]
        public async Task<FileResult> GetAsync([NotNull] string containerName, [NotNull] string blobName)
        {
            var blobContainer = _blobContainerFactory.Create(containerName);
            var stream = await blobContainer.GetOrNullAsync(blobName);
            var mimeType = GetMimeType(stream);
            return File(stream, mimeType);
        }

        [HttpPost]
        [Route("download/{containerName}/{blobName}")]
        public async Task<FileResult> DownloadAsync([NotNull] string containerName, [NotNull] string blobName, [NotNull] string fileName)
        {
            var blobContainer = _blobContainerFactory.Create(containerName);
            var stream = await blobContainer.GetOrNullAsync(blobName);
            var mimeType = GetMimeType(stream, fileName);
            return File(stream, mimeType, fileName);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="entityId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ListResultDto<BlobDto>> GetListAsync( string entityType,string entityId)
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
        public async Task DeleteByEntityAsync( string entityType,string entityId)
        {
            await _blobAppService.DeleteByEntityAsync(entityType, entityId);
        }


        /// <summary>
        /// 获取mimetype
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static string GetMimeType(Stream stream, string fileName = null)
        {
            var contentType = HeyRed.Mime.MimeGuesser.GuessMimeType(stream);
            if (string.IsNullOrEmpty(contentType))
            {
                if (!fileName.IsNullOrEmpty())
                    return HeyRed.Mime.MimeTypesMap.GetMimeType(fileName);
                else
                    return "application/octet-stream";
            }

            return contentType;
        }

    }
}
