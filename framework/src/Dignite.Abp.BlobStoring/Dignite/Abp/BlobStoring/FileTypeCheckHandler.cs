using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;

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
                string fileExtensionName = HeyRed.Mime.MimeGuesser.GuessExtension(context.BlobStream);

                if (!fileExtensionName.IsNullOrEmpty())
                {
                    if (fileExtensionName.EnsureStartsWith('.').ToLower() == ".zip")
                    {
                        fileExtensionName = CheckForMsOfficeTypes(context.BlobStream);
                    }

                    if (!fileTypeCheckHandlerConfiguration.AllowedFileTypeNames.Contains(fileExtensionName.EnsureStartsWith('.')))
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

        private static string CheckForMsOfficeTypes(Stream zip)
        {
            try
            {
                using (var zipFile = new ZipArchive(zip, ZipArchiveMode.Read,true))
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
    }
}
