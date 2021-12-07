using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.FieldCustomizing.FieldControls
{
    public interface IFieldControlProvider
    {
        /// <summary>
        /// Unique name of the field provider.
        /// </summary>
        string Name { get; }

        string DisplayName { get; }

        FieldControlType ControlType { get; }

        void Validate(FieldControlValidateArgs args);

        FieldControlConfigurationBase GetConfiguration(FieldControlConfigurationDictionary fieldControlConfiguration);
    }
}
