using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.BlobStoring
{
    /// <summary>
    /// 上传 BLOB 文件时进行文件类型检查
    /// </summary>
    public class FileTypeCheckHandler : IBlobProcessHandler,ITransientDependency
    {

        private readonly ICurrentBlobInfo _currentFile;

        public FileTypeCheckHandler(ICurrentBlobInfo currentFile)
        {
            _currentFile = currentFile;
        }

        public Task ProcessAsync(BlobProcessHandlerContext context)
        {
            var fileTypeCheckHandlerConfiguration = context.ContainerConfiguration.GetFileTypeCheckConfiguration();

            if (fileTypeCheckHandlerConfiguration.AllowedFileTypeNames != null && fileTypeCheckHandlerConfiguration.AllowedFileTypeNames.Length > 0)
            {
                //TODO : 需要注意：_currentFile.BlobInfo.BlobName 可能没有扩展名
                string fileExtensionName = Path.GetExtension(_currentFile.BlobInfo.BlobName);

                if (!fileExtensionName.IsNullOrEmpty())
                {
                    if (!fileTypeCheckHandlerConfiguration.AllowedFileTypeNames.Contains(fileExtensionName.ToLower()))
                    {
                        throw new BusinessException(
                            code: "Dignite.Abp.BlobStoring:010002",
                            message: "File type is incompatible!"+ "File type should be one of" + fileTypeCheckHandlerConfiguration.AllowedFileTypeNames.JoinAsString("/") + "!",
                            details: "File type should be one of" + fileTypeCheckHandlerConfiguration.AllowedFileTypeNames.JoinAsString("/") + "!"
                        );
                    }
                }
                else
                {
                    throw new BusinessException(
                        code: "Dignite.Abp.BlobStoring:010003",
                        message: "File type is unrecognized!",
                        details: "Cannot get the file type of uploaded file!"
                    );
                }
            }
            return Task.CompletedTask;
        }

    }
}
