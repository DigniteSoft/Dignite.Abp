using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using MimeDetective.InMemory;

namespace Dignite.Abp.BlobStoring
{
    /// <summary>
    /// 上传 BLOB 文件时进行文件类型检查
    /// </summary>
    public class FileTypeCheckHandler : IBlobProcessHandler
    {
        public Task ProcessAsync(BlobProcessHandlerContext context)
        {
            var fileTypeCheckHandlerConfiguration = context.ContainerConfiguration.GetFileTypeCheckConfiguration();

            // TODO: case sensitivity
            if (fileTypeCheckHandlerConfiguration.AllowedFileTypeNames != null && fileTypeCheckHandlerConfiguration.AllowedFileTypeNames.Length > 0)
            {
                var fileType = context.BlobStream.DetectMimeType();
                if (fileType != null)
                {
                    if (!fileTypeCheckHandlerConfiguration.AllowedFileTypeNames.Contains(fileType.Extension.EnsureStartsWith('.')))
                    {
                        throw new BusinessException(
                            code: "Dignite.Abp.BlobStoring:010002",
                            message: "File type is incompatible!",
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
