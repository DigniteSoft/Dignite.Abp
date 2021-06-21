using JetBrains.Annotations;
using System.Collections.Generic;

namespace Dignite.Abp.BlobStoringManagement
{
    public class DigniteBlobOptions
    {
        [NotNull]
        public List<BlobEntityTypeDefinition> EntityTypes { get; } = new List<BlobEntityTypeDefinition>();
    }
}
