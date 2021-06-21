using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Dignite.FieldCustomizing
{
    public class CustomizeFieldProviderSelector : ICustomizeFieldProviderSelector, ITransientDependency
    {
        protected IEnumerable<ICustomizeFieldProvider> FieldProviders { get; }

        public CustomizeFieldProviderSelector(
            IEnumerable<ICustomizeFieldProvider> blobProviders)
        {
            FieldProviders = blobProviders;
        }

        [NotNull]
        public virtual ICustomizeFieldProvider Get([NotNull] string providerName)
        {

            if (!FieldProviders.Any())
            {
                throw new AbpException("No field customize provider was registered! At least one provider must be registered to be able to use the blob customizing system.");
            }

            var provider = FieldProviders.SingleOrDefault(fp => fp.Name == providerName);

            if (provider == null)
                throw new AbpException(
                    $"Could not find the field customize provider with the name ({providerName}) ."
                );
            else
                return provider;
        }
    }
}