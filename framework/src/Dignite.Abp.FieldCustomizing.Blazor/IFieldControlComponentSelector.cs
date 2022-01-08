using Dignite.Abp.FieldCustomizing.FieldControls;
using JetBrains.Annotations;

namespace Dignite.Abp.FieldCustomizing.Blazor
{
    public interface IFieldControlComponentSelector
    {
        /// <summary>
        /// Get blazor component using field control provider name
        /// </summary>
        /// <param name="fieldControlProviderName">
        /// <see cref="IFieldControlProvider.Name"/>
        /// </param>
        /// <returns></returns>
        [NotNull]
        IFieldControlComponent Get(string fieldControlProviderName);
    }
}
