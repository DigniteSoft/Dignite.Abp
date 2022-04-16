using Dignite.Abp.BlobStoring;
using System;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace Dignite.Abp.FileManagement
{
    public class File : BasicAggregateRoot<Guid>, IBlobInfo, ICreationAuditedObject, IDeletionAuditedObject, IMultiTenant
    {
        protected File()
        { }

        public File(Guid id,string entityType,string entityId, BasicBlobInfo blobInfo, Guid? tenantId)
        {
            Id = id;
            EntityType = entityType;
            EntityId = entityId;
            ContainerName = blobInfo.ContainerName;
            BlobName = blobInfo.BlobName;
            BinarySize = blobInfo.BinarySize;
            Hash = blobInfo.Hash;
            ReferBlobName = blobInfo.ReferBlobName;
            TenantId = tenantId;
        }

        public virtual string EntityType { get; protected set; }

        public virtual string EntityId { get; protected set; }

        public string ContainerName { get; protected set; }

        public string BlobName { get; protected set; }

        public long BinarySize { get; protected set; }

        public string Hash { get; protected set; }

        public string ReferBlobName { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        public string FileName { get; set; }

        public DateTime CreationTime { get; set; }

        public Guid? CreatorId { get; set; }

        public Guid? DeleterId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }

        public Guid? TenantId { get; protected set; }

    }
}
