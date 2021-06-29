using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.FieldCustomizing
{
    public interface ICustomizeFieldFormProvider: ITransientDependency
    {
        /// <summary>
        /// Unique name of the field provider.
        /// </summary>
        string Name { get; }

        string DisplayName { get; }

        CustomizeFieldFormType FormType { get; }

        void Validate(CustomizeFieldFormValidateArgs args);

        CustomizeFieldFormProviderConfigurationBase GetConfiguration(CustomizeFieldFormConfiguration fieldFormConfiguration);
    }
}
