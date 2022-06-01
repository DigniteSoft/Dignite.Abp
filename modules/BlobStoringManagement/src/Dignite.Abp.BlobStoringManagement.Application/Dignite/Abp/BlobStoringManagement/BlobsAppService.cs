using Dignite.Abp.BlobStoring;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.BlobStoring;
using Volo.Abp.Content;

namespace Dignite.Abp.BlobStoringManagement
{
    public class BlobsAppService : ApplicationService, IBlobsAppService
    {
        private readonly IBlobContainerFactory _blobContainerFactory;
        private readonly IBlobContainerConfigurationProvider _configurationProvider;
        private readonly IBlobRepository _blobRepository;

        public BlobsAppService(
            IBlobContainerFactory blobContainerFactory,
            IBlobContainerConfigurationProvider configurationProvider,
            IBlobRepository blobRepository
            )
        {
            _blobContainerFactory = blobContainerFactory;
            _configurationProvider = configurationProvider;
            _blobRepository = blobRepository;
        }

        public async Task<BlobDto> SaveRemoteFileAsync([NotNull] string containerName, [NotNull] SaveRemoteFileInput input)
        {
            var blobContainer = _blobContainerFactory.Create(containerName);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(input.Url);
            request.Method = "GET";
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Accept = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*";
            request.Headers.Add("Accept-Language", "zh-cn");
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3029.110 Safari/537.36 SE 2.X MetaSr 1.0";
            request.Headers.Add("UA-CPU", "x86");

            using (WebResponse response = request.GetResponse())
            {
                //
                MemoryStream ms = new MemoryStream();
                response.GetResponseStream().CopyTo(ms);
                string fileExtensionName = GetFileExtensionName(ms);
                byte[] bytes = ms.ToArray();
                var blobName = await GeneratorNameAsync(containerName, fileExtensionName);

                await blobContainer.SaveAsync(blobName, bytes, true);

                //
                var blob = await _blobRepository.FindAsync(containerName, blobName);
                return ObjectMapper.Map<Blob, BlobDto>(blob);
            }
        }

        public async Task<BlobDto> SaveAsync([NotNull] string containerName, [NotNull] SaveStreamInput input)
        {
            using (var stream = new MemoryStream(input.FileStream.GetStream().GetAllBytes()))
            {
                var blobContainer = _blobContainerFactory.Create(containerName);
                string fileExtensionName = GetFileExtensionName(stream);
                var blobName = await GeneratorNameAsync(
                    containerName,
                    fileExtensionName
                    );

                await blobContainer.SaveAsync(blobName, stream, true);
                await CurrentUnitOfWork.SaveChangesAsync();

                //
                var blob = await _blobRepository.FindAsync(containerName, blobName);
                blob.BlobFileName = input.FileName;

                return ObjectMapper.Map<Blob, BlobDto>(blob);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public async Task<ListResultDto<BlobDto>> GetListAsync(string entityType, string entityId)
        {
            var result = await _blobRepository.GetListAsync(entityType, entityId);

            return new ListResultDto<BlobDto>(
                ObjectMapper.Map<List<Blob>, List<BlobDto>>(result)
                );
        }

        public async Task DeleteAsync([NotNull] string containerName, [NotNull] string blobName)
        {
            var blobContainer = _blobContainerFactory.Create(containerName);
            await blobContainer.DeleteAsync(blobName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public async Task DeleteByEntityAsync(string entityType, string entityId)
        {
            var result = await _blobRepository.GetListAsync(entityType, entityId);
            foreach (var blob in result)
            {
                var blobContainer = _blobContainerFactory.Create(blob.ContainerName);

                await blobContainer.DeleteAsync(blob.BlobName);
            }
        }

        [RemoteService(IsEnabled = false)]
        public async Task<Stream> GetOrNullAsync([NotNull] string containerName, [NotNull] string blobName)
        {
            var blobContainer = _blobContainerFactory.Create(containerName);
            return await blobContainer.GetOrNullAsync(blobName);
        }


        public virtual Task<BlobContainerConfigurationDto> GetBlobContainerConfigurationAsync([NotNull] string containerName)
        {
            var configuration = _configurationProvider.Get(containerName);
            var authorizationHandlerConfiguration = new AuthorizationHandlerConfiguration(configuration);
            var blobSizeLimitHandlerConfiguration = new BlobSizeLimitHandlerConfiguration(configuration);
            var fileTypeCheckHandlerConfiguration = new FileTypeCheckHandlerConfiguration(configuration);
            var imageProcessHandlerConfiguration = new ImageProcessHandlerConfiguration(configuration);

            return Task.FromResult(
                new BlobContainerConfigurationDto(
                    new AuthorizationHandlerConfigurationDto(authorizationHandlerConfiguration.SavingPolicy, authorizationHandlerConfiguration.SavingRoles, authorizationHandlerConfiguration.GettingPolicy, authorizationHandlerConfiguration.GettingRoles, authorizationHandlerConfiguration.DeletingPolicy, authorizationHandlerConfiguration.DeletingRoles),
                    new BlobSizeLimitHandlerConfigurationDto(blobSizeLimitHandlerConfiguration.MaximumBlobSize),
                    new FileTypeCheckHandlerConfigurationDto(fileTypeCheckHandlerConfiguration.AllowedFileTypeNames),
                    new ImageProcessHandlerConfigurationDto(imageProcessHandlerConfiguration.ImageWidth, imageProcessHandlerConfiguration.ImageHeight, imageProcessHandlerConfiguration.ImageSizeMustBeLargerThanPreset)
                    )
                );
        }

        private async Task<string> GeneratorNameAsync(string containerName, string extensionName = null)
        {
            var configuration = _configurationProvider.Get(containerName);
            var namingGeneratorType = configuration.GetConfigurationOrDefault(
                DigniteAbpBlobContainerConfigurationNames.NamingGenerator,
                typeof(SimpleBlobNameGenerator)
                );

            var generator = LazyServiceProvider.LazyGetRequiredService(namingGeneratorType)
                .As<IBlobNameGenerator>();

            var blobName = await generator.Create();
            return blobName + extensionName ?? string.Empty;
        }

        private static string GetFileExtensionName(Stream stream)
        {
            string fileExtensionName = HeyRed.Mime.MimeGuesser.GuessExtension(stream);

            if (fileExtensionName.EnsureStartsWith('.').ToLower() == ".zip")
            {
                try
                {
                    using (var zipFile = new ZipArchive(stream, ZipArchiveMode.Read, true))
                    {
                        if (zipFile.Entries.Any(e => e.FullName.StartsWith("word/")))
                            return ".docx";

                        if (zipFile.Entries.Any(e => e.FullName.StartsWith("xl/")))
                            return ".xlsx";

                        if (zipFile.Entries.Any(e => e.FullName.StartsWith("ppt/")))
                            return ".pptx";
                    }

                    return ".zip";
                }
                catch (InvalidDataException)
                {
                    return null;  //ZIP archive can be corrupted
                }
            }
            else
            {
                return fileExtensionName;
            }
        }

        public Task<BlobDto> UploadFileAsync([NotNull] string containerName, IRemoteStreamContent file, string EntityType, string EntityId)
        {
            throw new NotImplementedException();
        }
    }
}
