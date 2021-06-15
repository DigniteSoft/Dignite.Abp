
using Volo.Abp.DependencyInjection;

namespace Dignite.FieldCustomizing
{
    public class CustomizeFieldFactory : ITransientDependency
    {
        protected ICustomizeFieldProviderSelector ProviderSelector { get; }


        public CustomizeFieldFactory(
            ICustomizeFieldProviderSelector providerSelector)
        {
            ProviderSelector = providerSelector;
        }


        public virtual ICustomizeField Create(ICustomizeFieldDefinition fieldDefinition)
        {
            return new CustomizeField(
                fieldDefinition,
                ProviderSelector.Get(fieldDefinition.Configuration.ProviderName)
            );
        }
    }
}
