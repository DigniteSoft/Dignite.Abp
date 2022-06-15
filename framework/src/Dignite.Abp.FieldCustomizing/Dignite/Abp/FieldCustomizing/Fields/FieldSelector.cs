using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.FieldCustomizing.Fields
{
    public class FieldSelector : IFieldProviderSelector, ITransientDependency
    {
        protected IEnumerable<IFieldProvider> FieldProviders { get; }

        public FieldSelector(
            IEnumerable<IFieldProvider> fieldProviders)
        {
            FieldProviders = fieldProviders;
        }

        [NotNull]
        public virtual IFieldProvider Get([NotNull] string controlProviderName)
        {
            if (!FieldProviders.Any())
            {
                throw new AbpException("No field control provider was registered! At least one provider must be registered to be able to use the field customizing system.");
            }

            var fieldProvider = FieldProviders.SingleOrDefault(fp => fp.Name == controlProviderName);

            if (fieldProvider == null)
                throw new AbpException(
                    $"Could not find the field control provider with the name ({controlProviderName}) ."
                );
            else
                return fieldProvider;
        }
    }
}