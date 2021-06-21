
namespace Dignite.FieldCustomizing
{
    public interface ICustomizeFieldFactory
    {
        /// <summary>
        /// Create a field.
        /// </summary>
        /// <param name="fieldDefinition"></param>
        /// <returns></returns>
        ICustomizeField Create(
            ICustomizeFieldDefinition fieldDefinition
        );        
    }
}