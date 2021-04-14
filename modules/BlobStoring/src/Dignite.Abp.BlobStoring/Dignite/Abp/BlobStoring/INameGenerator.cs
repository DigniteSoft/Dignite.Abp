using System.Threading.Tasks;

namespace Dignite.Abp.BlobStoring
{
    /// <summary>
    /// Used to generate blob names.
    /// </summary>
    public interface INameGenerator
    {
        /// <summary>
        /// Creates a new blob name.
        /// </summary>
        Task<string> Create(string extensionName);
    }
}
