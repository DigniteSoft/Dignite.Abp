using JetBrains.Annotations;
using System.Collections.Generic;

namespace Dignite.Abp.BlobStoringManagement
{
    public class DigniteBlobEntityResolveOptions
    {
        [NotNull]
        public List<IBlobEntityResolveContributor> BlobEntityResolvers { get; }

        public DigniteBlobEntityResolveOptions()
        {
            BlobEntityResolvers = new List<IBlobEntityResolveContributor>();
        }
    }
}
