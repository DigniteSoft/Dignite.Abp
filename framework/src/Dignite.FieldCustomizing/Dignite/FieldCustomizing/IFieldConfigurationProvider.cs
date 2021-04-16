
namespace Dignite.FieldCustomizing
{
    public interface IFieldConfigurationProvider
    {
        /// <summary>
        /// Gets a <see cref="FieldConfiguration"/> for the given field <paramref name="name"/>.
        /// </summary>
        /// <param name="name">The name of the field</param>
        /// <returns>The configuration that should be used for the field</returns>
        FieldConfiguration Get(string name);
    }
}