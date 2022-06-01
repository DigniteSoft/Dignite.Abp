using System;
using Volo.Abp.Application.Dtos;

namespace Dignite.Abp.BlobStoringManagement
{
    public class BlobDto : CreationAuditedEntityDto<Guid>
    {
        public virtual string EntityType { get; protected set; }

        public virtual string EntityId { get; protected set; }

        public string ContainerName { get; protected set; }

        public string BlobName { get; protected set; }

        public string ReferBlobName { get; set; }
        public long BinarySize { get; protected set; }

        public string Hash { get; protected set; }
        public string BlobFileName { get; set; }
    }
}
