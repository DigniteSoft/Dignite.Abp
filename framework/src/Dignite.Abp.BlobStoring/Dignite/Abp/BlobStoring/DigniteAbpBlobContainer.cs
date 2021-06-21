using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.BlobStoring;
using Volo.Abp.Collections;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Threading;

namespace Dignite.Abp.BlobStoring
{
    public class DigniteAbpBlobContainer : BlobContainer
    {
        public DigniteAbpBlobContainer(
            string containerName,
            BlobContainerConfiguration configuration,
            IBlobProvider provider,
            ICurrentTenant currentTenant,
            ICancellationTokenProvider cancellationTokenProvider,
            IServiceProvider serviceProvider) :base(
                containerName,
                configuration,
                provider,
                currentTenant,
                cancellationTokenProvider,
                serviceProvider)
        {
        }

        public override async Task<bool> DeleteAsync(string name, CancellationToken cancellationToken = default)
        {
            var blobInfoStoreType = Configuration.GetConfigurationOrDefault(
                DigniteAbpBlobContainerConfigurationNames.BlobInfoStore,
                typeof(NullBlobInfoStore)
                );

            using (var scope = ServiceProvider.CreateScope())
            {
                var blobInfoStore = scope.ServiceProvider
                    .GetRequiredService(blobInfoStoreType)
                    .As<IBlobInfoStore>();

                var blobInfo = await blobInfoStore.FindAsync(ContainerName, name, cancellationToken);

                // authorization handlers
                await CheckGettingPermissionAsync(blobInfo);

                if (blobInfo != null)
                {
                    if (blobInfo.ReferBlobName.IsNullOrEmpty())
                    {
                        if (!await blobInfoStore.ReferenceExistsAsync(ContainerName, name,  cancellationToken))
                        {
                            return await base.DeleteAsync(name, cancellationToken);
                        }
                    }
                    else
                    {
                        if (!await blobInfoStore.ReferenceExistsAsync(ContainerName, blobInfo.ReferBlobName,  cancellationToken))
                        {
                            return await base.DeleteAsync(blobInfo.ReferBlobName, cancellationToken);
                        }
                    }

                    await blobInfoStore.DeleteAsync(ContainerName, name, cancellationToken);
                    return true;
                }
                else
                {
                    return await base.DeleteAsync(name, cancellationToken);
                }
            }

        }

        public override async Task<Stream> GetOrNullAsync(string name, CancellationToken cancellationToken = default)
        {


            var blobInfoStoreType = Configuration.GetConfigurationOrDefault(
                DigniteAbpBlobContainerConfigurationNames.BlobInfoStore,
                typeof(NullBlobInfoStore)
                );

            using (var scope = ServiceProvider.CreateScope())
            {
                var blobInfoStore = scope.ServiceProvider
                    .GetRequiredService(blobInfoStoreType)
                    .As<IBlobInfoStore>();

                var blobInfo = await blobInfoStore.FindAsync(ContainerName, name, cancellationToken);
                // authorization handlers
                await CheckDeletingPermissionAsync(blobInfo);

                //
                if (blobInfo != null)
                {
                    if (!blobInfo.ReferBlobName.IsNullOrEmpty())
                        return await base.GetOrNullAsync(blobInfo.ReferBlobName, cancellationToken);
                    else
                        return await base.GetOrNullAsync(name, cancellationToken);
                }
                else
                    return await base.GetOrNullAsync(name, cancellationToken);

            }
        }

        public override async Task SaveAsync(
            string name,
            Stream stream,
            bool overrideExisting = false,
            CancellationToken cancellationToken = default)
        {
            // authorization handlers
            await CheckSavingPermissionAsync();

            // blob process handlers
            await BlobProcessHandlers(stream);

            // save blob
            await HashAndSaveAsync(name, stream, overrideExisting, cancellationToken);

            // TODO:考虑使用Event Bus技术实现回调
        }

        private async Task CheckSavingPermissionAsync()
        {
            var authorizationHandlerType = Configuration.GetConfigurationOrDefault<Type>(DigniteAbpBlobContainerConfigurationNames.AuthorizationHandler, null);
            if (authorizationHandlerType != null)
            {
                using (var scope = ServiceProvider.CreateScope())
                {
                    var authorizationConfiguration = new AuthorizationHandlerConfiguration(Configuration);
                    var handler = scope.ServiceProvider
                            .GetRequiredService(authorizationHandlerType)
                            .As<IAuthorizationHandler>();

                        await handler.CheckSavingPermissionAsync(authorizationConfiguration);
                    
                }
            }
        }

        private async Task CheckGettingPermissionAsync(IBlobInfo blobInfo)
        {
            var authorizationHandlerType = Configuration.GetConfigurationOrDefault<Type>(DigniteAbpBlobContainerConfigurationNames.AuthorizationHandler, null);
            if (authorizationHandlerType != null)
            {
                using (var scope = ServiceProvider.CreateScope())
                {
                    var authorizationConfiguration = new AuthorizationHandlerConfiguration(Configuration);
                    var handler = scope.ServiceProvider
                            .GetRequiredService(authorizationHandlerType)
                            .As<IAuthorizationHandler>();

                    await handler.CheckGettingPermissionAsync(authorizationConfiguration,blobInfo);

                }
            }
        }


        private async Task CheckDeletingPermissionAsync(IBlobInfo blobInfo)
        {
            var authorizationHandlerType = Configuration.GetConfigurationOrDefault<Type>(DigniteAbpBlobContainerConfigurationNames.AuthorizationHandler, null);
            if (authorizationHandlerType != null)
            {
                using (var scope = ServiceProvider.CreateScope())
                {
                    var authorizationConfiguration = new AuthorizationHandlerConfiguration(Configuration);
                    var handler = scope.ServiceProvider
                            .GetRequiredService(authorizationHandlerType)
                            .As<IAuthorizationHandler>();

                    await handler.CheckDeletingPermissionAsync(authorizationConfiguration, blobInfo);

                }
            }
        }

        private async Task BlobProcessHandlers(Stream stream)
        {
            // blob process handlers
            var processHandlers = Configuration.GetConfigurationOrDefault<ITypeList<IBlobProcessHandler>>(DigniteAbpBlobContainerConfigurationNames.BlobProcessHandlers, null);
            if (processHandlers != null && processHandlers.Any())
            {
                var context = new BlobProcessHandlerContext(stream, Configuration, CurrentTenant, ServiceProvider);
                using (var scope = ServiceProvider.CreateScope())
                {
                    foreach (var handlerType in processHandlers)
                    {
                        var handler = scope.ServiceProvider
                            .GetRequiredService(handlerType)
                            .As<IBlobProcessHandler>();

                        await handler.ProcessAsync(context);
                    }
                }
            }
        }

        private async Task HashAndSaveAsync(
            string name,
            Stream stream,
            bool overrideExisting = false,
            CancellationToken cancellationToken = default)
        {
            var blobInfoStoreType = Configuration.GetConfigurationOrDefault(
                DigniteAbpBlobContainerConfigurationNames.BlobInfoStore,
                typeof(NullBlobInfoStore)
                );

            using (var scope = ServiceProvider.CreateScope())
            {
                var blobInfoStore = scope.ServiceProvider
                    .GetRequiredService(blobInfoStoreType)
                    .As<IBlobInfoStore>();

                if (blobInfoStore is NullBlobInfoStore)
                {
                    // 保存到容器中
                    await base.SaveAsync(name, stream, overrideExisting, cancellationToken);
                }
                else
                {
                    // 计算stream hash
                    var hash = stream.ToMd5();
                    var blobInfo = new BasicBlobInfo(ContainerName, name);
                    if (await blobInfoStore.HashExistsAsync(ContainerName, hash, cancellationToken))
                    {
                        // 如果存在相同hash的blob，则创建其副本
                        var mainBlob = await blobInfoStore.FindByHashAsync(ContainerName, hash, cancellationToken);
                        blobInfo.ReferBlobName = mainBlob.BlobName;

                        // 记录Blob信息
                        await blobInfoStore.CreateAsync(blobInfo, cancellationToken);
                    }
                    else
                    {
                        blobInfo.BinarySize = stream.Length;
                        blobInfo.Hash = hash;

                        // 记录Blob信息；
	                    //这里是在保存blob stream 前创建数据库记录，当后续发生异常时，希望能自动回滚本次操作；
                        await blobInfoStore.CreateAsync(blobInfo, cancellationToken);

                        // 保存到容器中
                        await base.SaveAsync(name, stream, overrideExisting, cancellationToken);
                    }
                }
            }
        }

    }
}
