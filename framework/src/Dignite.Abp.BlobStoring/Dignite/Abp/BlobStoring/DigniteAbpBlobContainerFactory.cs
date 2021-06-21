using System;
using Volo.Abp.BlobStoring;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Threading;

namespace Dignite.Abp.BlobStoring
{
    public class DigniteAbpBlobContainerFactory: BlobContainerFactory, ITransientDependency
    {

        public DigniteAbpBlobContainerFactory(
            IBlobContainerConfigurationProvider configurationProvider,
            ICurrentTenant currentTenant,
            ICancellationTokenProvider cancellationTokenProvider,
            IBlobProviderSelector providerSelector,
            IServiceProvider serviceProvider)
            :base(
                 configurationProvider,
                 currentTenant,
                 cancellationTokenProvider,
                 providerSelector,
                 serviceProvider
                 )
        {
        }

        public override IBlobContainer Create(string name)
        {
            var configuration = ConfigurationProvider.Get(name);

            return new DigniteAbpBlobContainer(
                name,
                configuration,
                ProviderSelector.Get(name),
                CurrentTenant,
                CancellationTokenProvider,
                ServiceProvider
            );
        }
    }
}
