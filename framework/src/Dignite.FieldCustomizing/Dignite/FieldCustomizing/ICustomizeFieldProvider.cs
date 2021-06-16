

namespace Dignite.FieldCustomizing
{
    public interface ICustomizeFieldProvider
    {
        /// <summary>
        /// Unique name of the field provider.
        /// </summary>
        string Name { get; }

        string DisplayName { get; }

        CustomizeFieldType FieldType { get; }

        void Validate(CustomizeFieldProviderValidateArgs args);
    }
}
