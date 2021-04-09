using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;

namespace Dignite.Abp.BlobStoring
{
    /// <summary>
    /// 上传BLOB文件时进行文件类型检查
    /// </summary>
    public class FileTypeCheckHandler : IBlobProcessHandler
    {
        public Task ProcessAsync(BlobProcessHandlerContext context)
        {
            var fileTypeCheckHandlerConfiguration = context.ContainerConfiguration.GetFileTypeCheckConfiguration();
            if (fileTypeCheckHandlerConfiguration.AllowedFileTypeNames != null && fileTypeCheckHandlerConfiguration.AllowedFileTypeNames.Length > 0)
            {
                string fileExtensionName = HeyRed.Mime.MimeGuesser.GuessExtension(context.BlobStream);
                if (!fileExtensionName.IsNullOrEmpty())
                {
                    if (!fileTypeCheckHandlerConfiguration.AllowedFileTypeNames.Contains(fileExtensionName))
                    {
                        //TODO:异常改为 BusinessException 
                        throw new UserFriendlyException("文件格式必须是 " + fileTypeCheckHandlerConfiguration.AllowedFileTypeNames.JoinAsString("/") + " 中的一种！");
                    }
                }
                else
                {
                    //TODO:异常改为 BusinessException 
                    throw new UserFriendlyException("未能获取到上传文件的文件格式");
                }
            }
            return Task.CompletedTask;
        }

    }
}
