

namespace Dignite.FieldCustomizing
{
    public interface IFieldFactory<TFieldConfigurationProvider>
        where TFieldConfigurationProvider : IFieldConfigurationProvider
    {
        /// <summary>
        /// Gets a named field.
        /// </summary>
        /// <param name="name">The name of the field</param>
        /// <returns>
        /// The field object.
        /// </returns>
        IField Create(
            string name
        );
    }
}