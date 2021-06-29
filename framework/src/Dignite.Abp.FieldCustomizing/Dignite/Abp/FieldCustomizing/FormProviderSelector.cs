using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.FieldCustomizing
{
    public class FormProviderSelector : IFormProviderSelector, ITransientDependency
    {
        protected IEnumerable<IFormProvider> FormProviders { get; }

        public FormProviderSelector(
            IEnumerable<IFormProvider> blobProviders)
        {
            FormProviders = blobProviders;
        }

        [NotNull]
        public virtual IFormProvider Get([NotNull] string formProviderName)
        {
            if (!FormProviders.Any())
            {
                throw new AbpException("No field form provider was registered! At least one provider must be registered to be able to use the field customizing system.");
            }

            var formProvider = FormProviders.SingleOrDefault(fp => fp.Name == formProviderName);

            if (formProvider == null)
                throw new AbpException(
                    $"Could not find the field form provider with the name ({formProviderName}) ."
                );
            else
                return formProvider;
        }
    }
}