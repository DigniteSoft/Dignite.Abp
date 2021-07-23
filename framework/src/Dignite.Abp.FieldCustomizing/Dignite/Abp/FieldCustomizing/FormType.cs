

namespace Dignite.Abp.FieldCustomizing
{
    public enum FormType
    {
        /// <summary>
        /// Simple type forms may not include other types of forms
        /// </summary>
        Simple,

        /// <summary>
        /// Complex type forms can include simple type forms
        /// </summary>
        Complex
    }
}
