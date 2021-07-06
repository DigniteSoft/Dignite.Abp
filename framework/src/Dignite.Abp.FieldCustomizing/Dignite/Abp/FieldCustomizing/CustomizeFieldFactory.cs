
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.FieldCustomizing
{
    public class CustomizeFieldFactory : ICustomizeFieldFactory,ITransientDependency
    {
        protected IFormProviderSelector FormProviderSelector { get; }


        public CustomizeFieldFactory(
            IFormProviderSelector providerSelector)
        {
            FormProviderSelector = providerSelector;
        }


        public virtual ICustomizeField Create(BasicCustomizeFieldDefinition fieldDefinition)
        {
            return new CustomizeField(
                fieldDefinition,
                FormProviderSelector.Get(fieldDefinition.FormConfiguration.FormProviderName)
            );
        }
    }
}
