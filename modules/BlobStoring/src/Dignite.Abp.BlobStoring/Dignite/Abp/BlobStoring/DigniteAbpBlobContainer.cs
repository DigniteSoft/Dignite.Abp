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
        protected IBlobInfoStore BlobInfoStore { get; }

        public DigniteAbpBlobContainer(
            string containerName,
            BlobContainerConfiguration configuration,
            IBlobProvider provider,
            ICurrentTenant currentTenant,
            ICancellationTokenProvider cancellationTokenProvider,
            IServiceProvider serviceProvider,
            IBlobInfoStore blobInfoStore) :base(
                containerName,
                configuration,
                provider,
                currentTenant,
                cancellationTokenProvider,
                serviceProvider)
        {
            BlobInfoStore = blobInfoStore;
        }

        public override async Task<bool> DeleteAsync(string name, CancellationToken cancellationToken = default)
        {
            // authorization handlers
            await AuthorizationCheckAsync();

            return await base.DeleteAsync(name, cancellationToken);
        }

        public override async Task<Stream> GetOrNullAsync(string name, CancellationToken cancellationToken = default)
        {
            // authorization handlers
            await AuthorizationCheckAsync();

            //
            return await base.GetOrNullAsync(name, cancellationToken);
        }

        public override async Task SaveAsync(
            string name,
            Stream stream,
            bool overrideExisting = false,
            CancellationToken cancellationToken = default)
        {
            // authorization handlers
            await AuthorizationCheckAsync();

            // blob process handlers
            await BlobProcessHandlers(stream);

            // 保存blob
            await HashAndSaveAsync(name, stream, overrideExisting, cancellationToken);

            // TODO:考虑使用Event Bus技术实现回调
        }

        private async Task AuthorizationCheckAsync()
        {
            // authorization handlers
            var savingHandlers = Configuration.GetConfigurationOrDefault<ITypeList<IAuthorizationHandler>>(DigniteAbpBlobContainerConfigurationNames.AuthorizationHandlers, null);
            if (savingHandlers != null && savingHandlers.Any())
            {
                using (var scope = ServiceProvider.CreateScope())
                {
                    foreach (var handlerType in savingHandlers)
                    {
                        var handler = scope.ServiceProvider
                            .GetRequiredService(handlerType)
                            .As<IAuthorizationHandler>();

                        await handler.CheckAsync(AuthorizationOperations.Getting, Configuration);
                    }
                }
            }
        }

        private async Task BlobProcessHandlers(Stream stream)
        {
            // blob process handlers
            var savingHandlers = Configuration.GetConfigurationOrDefault<ITypeList<IBlobProcessHandler>>(DigniteAbpBlobContainerConfigurationNames.BlobProcessHandlers, null);
            if (savingHandlers != null && savingHandlers.Any())
            {
                var context = new BlobProcessHandlerContext(stream, Configuration, CurrentTenant, ServiceProvider);
                using (var scope = ServiceProvider.CreateScope())
                {
                    foreach (var handlerType in savingHandlers)
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
                    var blobInfo = new BasicBlobInfo(ContainerName, name)
                    {
                        BinarySize = stream.Length,
                        Hash = hash,
                    };
                    if (await BlobInfoStore.AnyByHashAsync(ContainerName, hash))
                    {
                        // 如果存在相同hash的blob，则创建其副本
                        var mainBlob = await BlobInfoStore.GetMainAsync(ContainerName, hash);
                        blobInfo.ReferBlobName = mainBlob.BlobName;
                    }
                    else
                    {
                        // 保存到容器中
                        await base.SaveAsync(name, stream, overrideExisting, cancellationToken);
                    }

                    // 记录Blob信息
                    await BlobInfoStore.CreateAsync(blobInfo);
                }
            }
        }

    }
}
