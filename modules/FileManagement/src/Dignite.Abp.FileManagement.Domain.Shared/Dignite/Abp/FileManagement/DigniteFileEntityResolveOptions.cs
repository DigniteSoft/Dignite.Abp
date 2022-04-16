using JetBrains.Annotations;
using System.Collections.Generic;

namespace Dignite.Abp.FileManagement
{
    public class DigniteFileEntityResolveOptions
    {
        [NotNull]
        public List<IFileEntityResolveContributor> BlobEntityResolvers { get; }

        public DigniteFileEntityResolveOptions()
        {
            BlobEntityResolvers = new List<IFileEntityResolveContributor>();
        }
    }
}
