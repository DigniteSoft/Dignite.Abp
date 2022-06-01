﻿using JetBrains.Annotations;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace Dignite.Abp.BlobStoringManagement
{
    public interface IBlobsAppService : IApplicationService
    {
        /// <summary>
        /// save remote file
        /// </summary>
        /// <param name="containerName"></param>
        /// <param name="input"></param>
        /// <returns>
        /// Return blob url after successful saving
        /// </returns>
        Task<BlobDto> SaveRemoteFileAsync([NotNull] string containerName, SaveRemoteFileInput input);

        /// <summary>
        /// save file bytes
        /// </summary>
        /// <param name="containerName"></param>
        /// <param name="input"></param>
        /// <returns>
        /// Return blob url after successful saving
        /// </returns>
        Task<BlobDto> SaveAsync([NotNull] string containerName, SaveStreamInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="entityId"></param>
        /// <returns></returns>
        Task<ListResultDto<BlobDto>> GetListAsync([NotNull] string entityType, [NotNull] string entityId);


        Task DeleteAsync([NotNull] string containerName, [NotNull] string blobName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="entityId"></param>
        /// <returns></returns>
        Task DeleteByEntityAsync( [NotNull] string entityType, [NotNull] string entityId);

        [RemoteService(IsEnabled = false)]
        Task<Stream> GetOrNullAsync([NotNull] string containerName, [NotNull] string blobName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="containerName"></param>
        /// <returns></returns>
        Task<BlobContainerConfigurationDto> GetBlobContainerConfigurationAsync([NotNull] string containerName);
    }
}
