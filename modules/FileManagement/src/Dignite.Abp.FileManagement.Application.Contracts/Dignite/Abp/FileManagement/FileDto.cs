using System;
using Volo.Abp.Application.Dtos;

namespace Dignite.Abp.FileManagement
{
    public class FileDto: CreationAuditedEntityDto<Guid>
    {
        public virtual string EntityType { get; protected set; }

        public virtual string EntityId { get; protected set; }

        public string ContainerName { get; protected set; }

        public string BlobName { get; protected set; }

        public long BinarySize { get; protected set; }

        public string FileName { get; protected set; }
    }
}
