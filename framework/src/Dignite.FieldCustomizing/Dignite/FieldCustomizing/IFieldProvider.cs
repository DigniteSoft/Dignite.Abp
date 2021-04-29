using System.Threading.Tasks;
using Volo.Abp.Localization;

namespace Dignite.FieldCustomizing
{
    public interface IFieldProvider
    {
        /// <summary>
        /// Unique name of the field provider.
        /// </summary>
        string Name { get; }

        ILocalizableString DisplayName { get; }

        FieldProviderType Type { get; }

        Task<FieldValueValidateResult> ValidateAsync(FieldProviderValidateValueArgs args);

        FieldProviderConfigurationBase GetConfiguration(FieldConfiguration fieldConfiguration);
    }
}
