
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.FieldCustomizing
{
    public class CustomizeFieldFactory : ICustomizeFieldFactory,ITransientDependency
    {
        protected ICustomizeFieldFormProviderSelector FormProviderSelector { get; }


        public CustomizeFieldFactory(
            ICustomizeFieldFormProviderSelector providerSelector)
        {
            FormProviderSelector = providerSelector;
        }


        public virtual ICustomizeField Create(ICustomizeFieldDefinition fieldDefinition)
        {
            return new CustomizeField(
                fieldDefinition,
                FormProviderSelector.Get(fieldDefinition.FormConfiguration.FormProviderName)
            );
        }
    }
}
