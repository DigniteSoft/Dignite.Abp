using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Data;

namespace Dignite.Abp.AuditLoggingManagement.Application.Contracts.Dignite.Abp.AuditLoggingManagement
{
    public class AuditLogActionDto : EntityDto<Guid>
    {
        public virtual Guid? TenantId { get;  set; }

        public virtual Guid AuditLogId { get;  set; }

        public virtual string ServiceName { get;  set; }

        public virtual string MethodName { get;  set; }

        public virtual string Parameters { get;  set; }

        public virtual DateTime ExecutionTime { get;  set; }

        public virtual int ExecutionDuration { get;  set; }

        public virtual ExtraPropertyDictionary ExtraProperties { get;  set; }
    }
}
