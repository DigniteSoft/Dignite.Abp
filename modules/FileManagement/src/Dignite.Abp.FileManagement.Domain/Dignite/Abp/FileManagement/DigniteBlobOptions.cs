using JetBrains.Annotations;
using System.Collections.Generic;

namespace Dignite.Abp.FileManagement
{
    public class DigniteBlobOptions
    {
        [NotNull]
        public List<BlobEntityTypeDefinition> EntityTypes { get; } = new List<BlobEntityTypeDefinition>();
    }
}
