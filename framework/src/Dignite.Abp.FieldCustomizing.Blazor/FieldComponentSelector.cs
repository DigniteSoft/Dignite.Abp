using Dignite.Abp.FieldCustomizing.FieldControls;
using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.FieldCustomizing.Blazor
{
    public class FieldComponentSelector : IFieldComponentSelector, ITransientDependency
    {
        private readonly IEnumerable<IFieldComponent> _fieldComponents;
        private readonly IFieldControlProviderSelector _fieldControlProviderSelector;

        public FieldComponentSelector(IEnumerable<IFieldComponent> fieldComponents, IFieldControlProviderSelector fieldControlProviderSelector)
        {
            _fieldComponents = fieldComponents;
            _fieldControlProviderSelector = fieldControlProviderSelector;
        }


        /// <summary>
        /// Get blazor component using field control provider name
        /// </summary>
        /// <param name="fieldProviderName">
        /// <see cref="IFieldControlProvider.Name"/>
        /// </param>
        /// <returns></returns>
        [NotNull]
        public IFieldComponent Get(string fieldProviderName)
        {
            if (!_fieldComponents.Any())
            {
                throw new AbpException("No field component was registered! At least one component must be registered to be able to use the field customizing system.");
            }

            var fieldControlProvider = _fieldControlProviderSelector.Get(fieldProviderName);
            var fieldComponent = _fieldComponents.FirstOrDefault(fp => fp.FieldProviderType == fieldControlProvider.GetType());

            if (fieldComponent == null)
                throw new AbpException(
                    $"Could not find the field component with the field provider type full name ({fieldControlProvider.GetType().FullName}) ."
                );
            else
                return fieldComponent;
        }
    }
}
