using System;
using System.Threading.Tasks;

namespace Dignite.Abp.BlobStoring
{
    /// <summary>
    /// Implements <see cref="INameGenerator"/> by using <see cref="Guid.NewGuid"/>.
    /// </summary>
    public class SimpleNameGenerator:INameGenerator
    {
        public static SimpleNameGenerator Instance { get; } = new SimpleNameGenerator();

        public virtual Task<string> Create(string extensionName)
        {
            return Task.FromResult(
                Guid.NewGuid().ToString("N") + (extensionName.IsNullOrEmpty() ? "" : extensionName.EnsureStartsWith('.'))
                );
        }
    }
}
