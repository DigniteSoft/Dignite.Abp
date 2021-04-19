using Dignite.FieldCustomizing.Localization;
using System.Threading.Tasks;
using Volo.Abp.Localization;

namespace Dignite.FieldCustomizing
{
    public abstract class FieldProviderBase : IFieldProvider
    {
        public abstract string Name { get; }

        public abstract ILocalizableString DisplayName { get; }

        public abstract Task<FieldValueValidateResult> ValidateValueAsync(FieldProviderValidateValueArgs args);


        public abstract FieldProviderConfigurationBase GetConfiguration(FieldConfiguration fieldConfiguration);

        protected static LocalizableString L(string name)
        {
            return LocalizableString.Create<FieldCustomizingResource>(name);
        }
    }
}
