using JetBrains.Annotations;
using System.Threading.Tasks;

namespace Dignite.Abp.FileManagement
{
    public interface IBlobEntityResolver
    {
        /// <summary>
        /// Tries to resolve blob's entity using registered <see cref="IBlobEntityResolveContributor"/> implementations.
        /// </summary>
        /// <returns>
        /// Entity type, id or null (if could not resolve).
        /// </returns>
        [NotNull]
        Task<BlobEntityResolveResult> ResolveBlobEntityAsync();
    }
}
