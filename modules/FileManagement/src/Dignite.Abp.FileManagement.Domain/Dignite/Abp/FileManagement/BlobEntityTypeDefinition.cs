using JetBrains.Annotations;
using System;
using Volo.Abp;

namespace Dignite.Abp.FileManagement
{
    public class BlobEntityTypeDefinition : IEquatable<BlobEntityTypeDefinition>
    {
        public BlobEntityTypeDefinition([NotNull] string entityType)
        {
            EntityType = Check.NotNullOrEmpty(entityType, nameof(entityType));
        }

        [NotNull]
        public string EntityType { get; protected set; }

        public bool Equals(BlobEntityTypeDefinition other)
        {
            return EntityType == other?.EntityType;
        }
    }
}
