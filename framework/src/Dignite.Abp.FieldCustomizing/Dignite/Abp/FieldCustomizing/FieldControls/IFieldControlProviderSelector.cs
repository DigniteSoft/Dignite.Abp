using JetBrains.Annotations;

namespace Dignite.Abp.FieldCustomizing.FieldControls
{
    public interface IFieldControlProviderSelector
    {
        /// <summary>
        /// Get provider using field name
        /// </summary>
        /// <param name="providerName">
        /// <see cref="IFieldControlProvider.Name"/>
        /// </param>
        /// <returns></returns>
        [NotNull]
        IFieldControlProvider Get(string providerName);
    }
}
