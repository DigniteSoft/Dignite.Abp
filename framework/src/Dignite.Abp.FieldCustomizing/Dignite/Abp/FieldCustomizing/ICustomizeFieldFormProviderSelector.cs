using JetBrains.Annotations;

namespace Dignite.Abp.FieldCustomizing
{
    public interface ICustomizeFieldFormProviderSelector
    {
        /// <summary>
        /// Get provider using field name
        /// </summary>
        /// <param name="providerName">
        /// <see cref="ICustomizeFieldFormProvider.Name"/>
        /// </param>
        /// <returns></returns>
        [NotNull]
        ICustomizeFieldFormProvider Get(string providerName);
    }
}
