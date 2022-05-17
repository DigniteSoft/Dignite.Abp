using JetBrains.Annotations;
using System.Collections.Generic;

namespace Dignite.Abp.FileManagement
{
    public class FileOptions
    {
        [NotNull]
        public List<FileEntityTypeDefinition> EntityTypes { get; } = new List<FileEntityTypeDefinition>();
    }
}
