using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Dignite.FieldCustomizing
{
    public class FieldProviderSelector : IFieldProviderSelector, ITransientDependency
    {
        protected IEnumerable<IFieldProvider> FieldProviders { get; }

        public FieldProviderSelector(
            IEnumerable<IFieldProvider> blobProviders)
        {
            FieldProviders = blobProviders;
        }

        [NotNull]
        public virtual IFieldProvider Get([NotNull] string providerName)
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