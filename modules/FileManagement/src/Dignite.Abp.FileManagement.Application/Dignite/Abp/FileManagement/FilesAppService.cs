﻿using Dignite.Abp.BlobStoring;
using HeyRed.Mime;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
using Volo.Abp.Validation;

namespace Dignite.Abp.FileManagement
{
    public class FilesAppService : ApplicationService, IFilesAppService
    {
        private readonly IBlobContainerFactory _blobContainerFactory;
        private readonly IBlobContainerConfigurationProvider _configurationProvider;
        private readonly IFileRepository _blobRepository;

        public FilesAppService(
            IBlobContainerFactory blobContainerFactory,
            IBlobContainerConfigurationProvider configurationProvider,
            IFileRepository blobRepository
            )
        {
            _blobContainerFactory = blobContainerFactory;
            _configurationProvider = configurationProvider;
            _blobRepository = blobRepository;
        }

        public async Task<FileDto> SaveAsync([NotNull] string containerName, [NotNull] SaveRemoteFileInput input)
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
                var blobName = await GeneratorNameAsync(containerName, fileExtensionName);

                await blobContainer.SaveAsync(blobName, ms, true);

                //
                var blob = await _blobRepository.FindAsync(containerName, blobName);
                return ObjectMapper.Map<File, FileDto>(blob);
            }
        }



        /// <summary>
        /// Save Blob
        /// </summary>
        /// <param name="containerName"></param>
        /// <param name="input"></param>
        /// <returns>
        /// Return blob url after successful saving
        /// </returns>
        public async Task<FileDto> SaveAsync([NotNull] string containerName, SaveStreamInput input)
        {
            if (input.File == null)
            {
                ThrowValidationException("Bytes of file can not be null or empty!", nameof(input.File));
            }

            var stream = input.File.GetStream();
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
            blob.FileName = input.File.FileName;

            return ObjectMapper.Map<File, FileDto>(blob);
        }

        public virtual async Task<IRemoteStreamContent> GetFileAsync([NotNull] string containerName, [NotNull] string blobName)
        {
            var blobContainer = _blobContainerFactory.Create(containerName);
            var fileStream = await blobContainer.GetAsync(blobName);
            var mimeType = MimeTypesMap.GetMimeType(blobName);

            return new RemoteStreamContent(fileStream, blobName, mimeType, disposeStream: true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public async Task<ListResultDto<FileDto>> GetListAsync(string entityType, string entityId)
        {
            var result = await _blobRepository.GetListAsync(entityType, entityId);

            return new ListResultDto<FileDto>(
                ObjectMapper.Map<List<File>, List<FileDto>>(result)
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



        public virtual Task<BlobContainerConfigurationDto> GetBlobContainerConfigurationAsync([NotNull] string containerName)
        {
            var configuration = _configurationProvider.Get(containerName);
            var authorizationHandlerConfiguration = new AuthorizationHandlerConfiguration(configuration);
            var blobSizeLimitHandlerConfiguration = new BlobSizeLimitHandlerConfiguration(configuration);
            var fileTypeCheckHandlerConfiguration = new FileTypeCheckHandlerConfiguration(configuration);
            var imageProcessHandlerConfiguration = new ImageProcessHandlerConfiguration(configuration);

            return Task.FromResult(
                new BlobContainerConfigurationDto(
                    new AuthorizationHandlerConfigurationDto(authorizationHandlerConfiguration.SavingPolicy,authorizationHandlerConfiguration.SavingRoles,authorizationHandlerConfiguration.GettingPolicy,authorizationHandlerConfiguration.GettingRoles,authorizationHandlerConfiguration.DeletingPolicy,authorizationHandlerConfiguration.DeletingRoles),
                    new BlobSizeLimitHandlerConfigurationDto(blobSizeLimitHandlerConfiguration.MaximumBlobSize),
                    new FileTypeCheckHandlerConfigurationDto(fileTypeCheckHandlerConfiguration.AllowedFileTypeNames),
                    new ImageProcessHandlerConfigurationDto(imageProcessHandlerConfiguration.ImageWidth,imageProcessHandlerConfiguration.ImageHeight,imageProcessHandlerConfiguration.ImageSizeMustBeLargerThanPreset)
                    )
                ) ;
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

            var blobName= await generator.Create();
            return blobName+extensionName ?? string.Empty;
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
            else {
                return fileExtensionName.EnsureStartsWith('.');
            }
        }

        private static void ThrowValidationException(string message, string memberName)
        {
            throw new AbpValidationException(message,
                new List<ValidationResult>
                {
                    new ValidationResult(message, new[] {memberName})
                });
        }
    }
}
