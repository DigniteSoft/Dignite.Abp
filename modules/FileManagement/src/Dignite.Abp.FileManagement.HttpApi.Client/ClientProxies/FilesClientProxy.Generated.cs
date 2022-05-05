// This file is automatically generated by ABP framework to use MVC Controllers from CSharp
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Http.Client;
using Volo.Abp.Http.Modeling;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Http.Client.ClientProxying;
using Dignite.Abp.FileManagement;
using Volo.Abp.Content;

// ReSharper disable once CheckNamespace
namespace Dignite.Abp.FileManagement.ClientProxies;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(IFilesAppService), typeof(FilesClientProxy))]
public partial class FilesClientProxy : ClientProxyBase<IFilesAppService>, IFilesAppService
{
    public virtual async Task<FileDto> SaveUrlAsync(string containerName, SaveRemoteFileInput input)
    {
        return await RequestAsync<FileDto>(nameof(SaveUrlAsync), new ClientProxyRequestTypeValue
        {
            { typeof(string), containerName },
            { typeof(SaveRemoteFileInput), input }
        });
    }

    public virtual async Task<FileDto> SaveAsync(string containerName, SaveStreamInput input)
    {
        return await RequestAsync<FileDto>(nameof(SaveAsync), new ClientProxyRequestTypeValue
        {
            { typeof(string), containerName },
            { typeof(SaveStreamInput), input }
        });
    }

    public virtual async Task<BlobContainerConfigurationDto> GetBlobContainerConfigurationAsync(string containerName)
    {
        return await RequestAsync<BlobContainerConfigurationDto>(nameof(GetBlobContainerConfigurationAsync), new ClientProxyRequestTypeValue
        {
            { typeof(string), containerName }
        });
    }

    public virtual async Task<IRemoteStreamContent> GetFileAsync(string containerName, string blobName)
    {
        return await RequestAsync<IRemoteStreamContent>(nameof(GetFileAsync), new ClientProxyRequestTypeValue
        {
            { typeof(string), containerName },
            { typeof(string), blobName }
        });
    }

    public virtual async Task<ListResultDto<FileDto>> GetListAsync(string entityType, string entityId)
    {
        return await RequestAsync<ListResultDto<FileDto>>(nameof(GetListAsync), new ClientProxyRequestTypeValue
        {
            { typeof(string), entityType },
            { typeof(string), entityId }
        });
    }

    public virtual async Task DeleteAsync(string containerName, string blobName)
    {
        await RequestAsync(nameof(DeleteAsync), new ClientProxyRequestTypeValue
        {
            { typeof(string), containerName },
            { typeof(string), blobName }
        });
    }

    public virtual async Task DeleteByEntityAsync(string entityType, string entityId)
    {
        await RequestAsync(nameof(DeleteByEntityAsync), new ClientProxyRequestTypeValue
        {
            { typeof(string), entityType },
            { typeof(string), entityId }
        });
    }
}
