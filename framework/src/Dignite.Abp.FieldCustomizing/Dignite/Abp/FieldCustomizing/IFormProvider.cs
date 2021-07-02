using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.FieldCustomizing
{
    public interface IFormProvider
    {
        /// <summary>
        /// Unique name of the field provider.
        /// </summary>
        string Name { get; }

        string DisplayName { get; }

        CustomizeFieldFormType FormType { get; }

        void Validate(CustomizeFieldFormValidateArgs args);

        FormConfigurationBase GetConfiguration(CustomizeFieldFormConfiguration fieldFormConfiguration);
    }
}
