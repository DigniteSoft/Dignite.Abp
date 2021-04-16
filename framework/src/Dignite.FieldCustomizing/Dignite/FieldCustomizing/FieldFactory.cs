using Microsoft.Extensions.DependencyInjection;
using System;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Threading;

namespace Dignite.FieldCustomizing
{
    public class FieldFactory<TFieldConfigurationProvider>:IFieldFactory<TFieldConfigurationProvider>, ITransientDependency
        where TFieldConfigurationProvider : IFieldConfigurationProvider
    {
        protected IFieldProviderSelector ProviderSelector { get; }

        protected ICancellationTokenProvider CancellationTokenProvider { get; }

        protected IServiceProvider ServiceProvider { get; }


        public FieldFactory(
            ICancellationTokenProvider cancellationTokenProvider,
            IFieldProviderSelector providerSelector,
            IServiceProvider serviceProvider)
        {
            CancellationTokenProvider = cancellationTokenProvider;
            ProviderSelector = providerSelector;
            ServiceProvider = serviceProvider;
        }

        public virtual IField Create(string name)
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                var configurationProvider = scope.ServiceProvider
                    .GetRequiredService(typeof(TFieldConfigurationProvider))
                    .As<IFieldConfigurationProvider>();

                var configuration = configurationProvider.Get(name);
                return new Field(
                    name,
                    configuration,
                    ProviderSelector.Get(configuration.ProviderName),
                    CancellationTokenProvider,
                    ServiceProvider
                );
            }
        }
    }
}
