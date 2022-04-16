using JetBrains.Annotations;
using System;
using Volo.Abp;

namespace Dignite.Abp.FileManagement
{
    public class FileEntityTypeDefinition : IEquatable<FileEntityTypeDefinition>
    {
        public FileEntityTypeDefinition([NotNull] string entityType)
        {
            EntityType = Check.NotNullOrEmpty(entityType, nameof(entityType));
        }

        [NotNull]
        public string EntityType { get; protected set; }

        public bool Equals(FileEntityTypeDefinition other)
        {
            return EntityType == other?.EntityType;
        }
    }
}
