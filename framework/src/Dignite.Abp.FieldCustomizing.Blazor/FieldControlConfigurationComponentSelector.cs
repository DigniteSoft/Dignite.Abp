using Dignite.Abp.FieldCustomizing.FieldControls;
using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.FieldCustomizing.Blazor
{
    public class FieldControlConfigurationComponentSelector : IFieldControlConfigurationComponentSelector, ITransientDependency
    {
        private readonly IEnumerable<IFieldControlConfigurationComponent> _fieldControlConfigurationComponents;
        private readonly IFieldControlProviderSelector _fieldControlProviderSelector;

        public FieldControlConfigurationComponentSelector(IEnumerable<IFieldControlConfigurationComponent> fieldControlConfigurationComponents, IFieldControlProviderSelector fieldControlProviderSelector)
        {
            _fieldControlConfigurationComponents = fieldControlConfigurationComponents;
            _fieldControlProviderSelector = fieldControlProviderSelector;
        }


        /// <summary>
        /// Get blazor component using field control provider name
        /// </summary>
        /// <param name="fieldControlProviderName">
        /// <see cref="IFieldControlProvider.Name"/>
        /// </param>
        /// <returns></returns>
        [NotNull]
        public IFieldControlConfigurationComponent Get(string fieldControlProviderName)
        {
            if (!_fieldControlConfigurationComponents.Any())
            {
                throw new AbpException("No field control component was registered! At least one component must be registered to be able to use the field customizing system.");
            }

            var fieldControlProvider = _fieldControlProviderSelector.Get(fieldControlProviderName);
            var fieldControlComponent = _fieldControlConfigurationComponents.FirstOrDefault(fp => fp.FieldControlProviderType == fieldControlProvider.GetType());

            if (fieldControlComponent == null)
                throw new AbpException(
                    $"Could not find the field control component with the field control provider type full name ({fieldControlProvider.GetType().FullName}) ."
                );
            else
                return fieldControlComponent;
        }
    }
}
