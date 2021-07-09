
namespace Dignite.Abp.FieldCustomizing
{
    public interface ICustomizeFieldFactory
    {
        /// <summary>
        /// Create a field.
        /// </summary>
        /// <param name="fieldDefinition"></param>
        /// <returns></returns>
        ICustomizeField Create(
            BasicCustomizeFieldDefinition fieldDefinition
        );        
    }
}