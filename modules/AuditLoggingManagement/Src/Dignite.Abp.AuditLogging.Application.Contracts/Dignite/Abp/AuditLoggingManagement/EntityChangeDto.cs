using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Auditing;
using Volo.Abp.Data;

namespace Dignite.Abp.AuditLoggingManagement.Application.Contracts.Dignite.Abp.AuditLoggingManagement
{
    public class EntityChangeDto : EntityDto<Guid>
    {
        public Guid AuditLogId { get; set; }

        public Guid? TenantId { get; set; }

        public DateTime ChangeTime { get; set; }

        public EntityChangeType ChangeType { get; set; }

        public Guid? EntityTenantId { get; set; }

        public string EntityId { get; set; }

        public string EntityTypeFullName { get; set; }

        public ICollection<EntityPropertyChangeDto> PropertyChanges { get; set; }

        public ExtraPropertyDictionary ExtraProperties { get; set; }
    }
}
