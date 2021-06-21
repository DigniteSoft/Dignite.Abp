using JetBrains.Annotations;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

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
        Task<string> SaveRemoteFileAsync([NotNull] string containerName, SaveRemoteFileInput input);

        /// <summary>
        /// save file bytes
        /// </summary>
        /// <param name="containerName"></param>
        /// <param name="input"></param>
        /// <returns>
        /// Return blob url after successful saving
        /// </returns>
        Task<string> SaveAsync([NotNull] string containerName, SaveBytesInput input);

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
    }
}
