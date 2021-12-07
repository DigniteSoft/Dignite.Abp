using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.FieldCustomizing.FieldControls
{
    public class FieldControlProviderSelector : IFieldControlProviderSelector, ITransientDependency
    {
        protected IEnumerable<IFieldControlProvider> FieldControlProviders { get; }

        public FieldControlProviderSelector(
            IEnumerable<IFieldControlProvider> fieldControlProviders)
        {
            FieldControlProviders = fieldControlProviders;
        }

        [NotNull]
        public virtual IFieldControlProvider Get([NotNull] string controlProviderName)
        {
            if (!FieldControlProviders.Any())
            {
                throw new AbpException("No field control provider was registered! At least one provider must be registered to be able to use the field customizing system.");
            }

            var fieldControlProvider = FieldControlProviders.SingleOrDefault(fp => fp.Name == controlProviderName);

            if (fieldControlProvider == null)
                throw new AbpException(
                    $"Could not find the field control provider with the name ({controlProviderName}) ."
                );
            else
                return fieldControlProvider;
        }
    }
}