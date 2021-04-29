using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.BlobStoring;

namespace Dignite.Abp.BlobStoring
{
    [Area("blobStoring")]
    [RemoteService(Name = BlobStoringRemoteServiceConsts.RemoteServiceName)]
    [Route("api/blob-storing")]
    [ControllerName(BlobStoringRemoteServiceConsts.RemoteServiceName)]
    public class BlobStoringController : AbpController, IBlobStoringAppService
    {
        private readonly IBlobContainerFactory _blobContainerFactory;
        private readonly IBlobStoringAppService _blobTransferAppService;

        public BlobStoringController(
            IBlobContainerFactory blobContainerFactory,
            IBlobStoringAppService blobTransferAppService
            )
        {
            _blobContainerFactory = blobContainerFactory;
            _blobTransferAppService = blobTransferAppService;
        }


        [HttpPost]
        [Route("save-remote-file/{containerName}")]
        public async Task<string> RemoteFileSaveAsBlobAsync([NotNull] string containerName, [NotNull] string url)
        {
            return await _blobTransferAppService.RemoteFileSaveAsBlobAsync(containerName, url);
        }


        [HttpPost]
        [Route("save/{containerName}")]
        public async Task<string> SaveBlobAsync([NotNull] string containerName, [NotNull] byte[] bytes)
        {
            return await _blobTransferAppService.SaveBlobAsync(containerName, bytes);
        }


        [HttpPost]
        [Route("upload/{containerName}")]
        public async Task<string> UploadAsync([NotNull] string containerName, IFormFile file)
        {
            return await _blobTransferAppService.SaveBlobAsync(containerName, file.GetAllBytes());
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
        public async Task<FileResult> DownloadAsync([NotNull] string containerName, [NotNull] string blobName, string fileDownloadName)
        {
            var blobContainer = _blobContainerFactory.Create(containerName);
            var stream = await blobContainer.GetOrNullAsync(blobName);
            var mimeType = GetMimeType(stream, fileDownloadName);
            return File(stream, mimeType, fileDownloadName);
        }


        /// <summary>
        /// 获取mimetype
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static string GetMimeType(Stream stream, string fileName=null)
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
