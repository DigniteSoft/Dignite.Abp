using JetBrains.Annotations;

namespace Dignite.Abp.FieldCustomizing
{
    public interface ICustomizeFieldProviderSelector
    {
        /// <summary>
        /// Get provider using field name
        /// </summary>
        /// <param name="providerName">
        /// <see cref="ICustomizeFieldProvider.Name"/>
        /// </param>
        /// <returns></returns>
        [NotNull]
        ICustomizeFieldProvider Get(string providerName);
    }
}
