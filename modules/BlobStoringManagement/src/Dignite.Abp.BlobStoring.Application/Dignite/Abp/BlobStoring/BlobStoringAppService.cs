using Dignite.Abp.BlobStoring.Localization;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.BlobStoring;

namespace Dignite.Abp.BlobStoring
{
    public class BlobStoringAppService : ApplicationService, IBlobStoringAppService
    {
        private readonly IBlobContainerFactory _blobContainerFactory;
        private readonly IBlobContainerConfigurationProvider _configurationProvider;

        public BlobStoringAppService(
            IBlobContainerFactory blobContainerFactory,
            IBlobContainerConfigurationProvider configurationProvider
            )
        {
            _blobContainerFactory = blobContainerFactory;
            _configurationProvider = configurationProvider;
            LocalizationResource = typeof(BlobStoringResource);
        }

        public async Task<string> RemoteFileSaveAsBlobAsync([NotNull] string containerName, [NotNull] string url)
        {
            var blobContainer = _blobContainerFactory.Create(containerName);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
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
                var extensionName= HeyRed.Mime.MimeGuesser.GuessExtension(bytes);
                var blobName = await GeneratorNameAsync(containerName, extensionName);

                await blobContainer.SaveAsync(blobName, bytes, true);
                return $"{containerName}/{blobName}";
            }
        }

        public async Task<string> SaveBlobAsync([NotNull] string containerName, [NotNull] byte[] bytes)
        {
            using (var stream = new MemoryStream(bytes))
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

        private async Task<string> GeneratorNameAsync(string containerName, string extensionName = null)
        {
            var configuration = _configurationProvider.Get(containerName);
            var namingGeneratorType = configuration.GetConfigurationOrDefault(
                DigniteAbpBlobContainerConfigurationNames.NamingGenerator, 
                typeof(SimpleNameGenerator)
                );

            using (var scope = ServiceProvider.CreateScope())
            {
                var generator = scope.ServiceProvider
                    .GetRequiredService(namingGeneratorType)
                    .As<INameGenerator>();
                return await generator.Create(extensionName);
            }
        }
    }
}
