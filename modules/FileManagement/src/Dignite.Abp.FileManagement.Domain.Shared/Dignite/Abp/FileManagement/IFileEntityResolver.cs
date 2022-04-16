using JetBrains.Annotations;
using System.Threading.Tasks;

namespace Dignite.Abp.FileManagement
{
    public interface IFileEntityResolver
    {
        /// <summary>
        /// Tries to resolve blob's entity using registered <see cref="IFileEntityResolveContributor"/> implementations.
        /// </summary>
        /// <returns>
        /// Entity type, id or null (if could not resolve).
        /// </returns>
        [NotNull]
        Task<FileEntityResolveResult> ResolveBlobEntityAsync();
    }
}
