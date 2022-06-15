using Dignite.Abp.FieldCustomizing.FieldControls;
using JetBrains.Annotations;

namespace Dignite.Abp.FieldCustomizing.Blazor
{
    public interface IFieldComponentSelector
    {
        /// <summary>
        /// Get blazor component using field control provider name
        /// </summary>
        /// <param name="fieldProviderName">
        /// <see cref="IFieldControlProvider.Name"/>
        /// </param>
        /// <returns></returns>
        [NotNull]
        IFieldComponent Get(string fieldProviderName);
    }
}
