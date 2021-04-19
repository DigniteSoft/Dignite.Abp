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

        Task<FieldValueValidateResult> ValidateValueAsync(FieldProviderValidateValueArgs args);

        FieldProviderConfigurationBase GetConfiguration(FieldConfiguration fieldConfiguration);
    }
}
