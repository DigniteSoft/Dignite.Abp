using System;
using Volo.Abp.BlobStoring;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Threading;

namespace Dignite.Abp.BlobStoring
{
    public class DigniteAbpBlobContainerFactory : BlobContainerFactory, ITransientDependency
    {

        public DigniteAbpBlobContainerFactory(
            IBlobContainerConfigurationProvider configurationProvider,
            ICurrentTenant currentTenant,
            ICancellationTokenProvider cancellationTokenProvider,
            IBlobProviderSelector providerSelector,
            IServiceProvider serviceProvider,
            IBlobNormalizeNamingService blobNormalizeNamingService)
            : base(
                 configurationProvider,
                 currentTenant,
                 cancellationTokenProvider,
                 providerSelector,
                 serviceProvider,
                 blobNormalizeNamingService
                 )
        {
        }

        public override IBlobContainer Create(string name)
        {
            var configuration = ConfigurationProvider.Get(name);
            DigniteAbpBlobContainer result = null;
            var provider = ProviderSelector.Get(name);
            result = new DigniteAbpBlobContainer(
                  name,
                  configuration,
                  provider,
                  CurrentTenant,
                  CancellationTokenProvider,
                  BlobNormalizeNamingService,
                  ServiceProvider
              );
            return result;
        }
    }
}
