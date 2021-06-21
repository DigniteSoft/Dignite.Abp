using Dignite.Abp.BlobStoring;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.BlobStoring;

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

        public async Task<string> SaveRemoteFileAsync([NotNull] string containerName, [NotNull] SaveRemoteFileInput input)
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
                byte[] bytes = ms.ToArray();
                var extensionName = HeyRed.Mime.MimeGuesser.GuessExtension(bytes);
                var blobName = await GeneratorNameAsync(containerName, extensionName);

                await blobContainer.SaveAsync(blobName, bytes, true);
                return $"{containerName}/{blobName}";
            }
        }

        public async Task<string> SaveAsync([NotNull] string containerName, [NotNull] SaveBytesInput input)
        {
            using (var stream = new MemoryStream(input.Bytes))
            {
                var blobContainer = _blobContainerFactory.Create(containerName);
                var blobName = await GeneratorNameAsync(
                    containerName,
                    HeyRed.Mime.MimeGuesser.GuessExtension(stream)
                    );

                await blobContainer.SaveAsync(blobName, stream, true);
                return $"{containerName}/{blobName}";
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

        private async Task<string> GeneratorNameAsync(string containerName, string extensionName = null)
        {
            var configuration = _configurationProvider.Get(containerName);
            var namingGeneratorType = configuration.GetConfigurationOrDefault(
                DigniteAbpBlobContainerConfigurationNames.NamingGenerator,
                typeof(SimpleNameGenerator)
                );

            var generator = LazyServiceProvider.LazyGetRequiredService(namingGeneratorType)
                .As<INameGenerator>();

            return await generator.Create(extensionName);
        }
    }
}
