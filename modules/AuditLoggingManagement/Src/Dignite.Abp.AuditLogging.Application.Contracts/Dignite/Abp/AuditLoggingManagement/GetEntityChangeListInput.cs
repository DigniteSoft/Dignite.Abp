using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Volo.Abp.Auditing;

namespace Dignite.Abp.AuditLoggingManagement.Application.Contracts.Dignite.Abp.AuditLoggingManagement
{
    public class GetEntityChangeListInput
    {
        public string Sorting { get; set; } = null;
        public int MaxResultCount { get; set; } = 50;
        public int SkipCount { get; set; } = 0;
        public Guid? AuditLogId { get; set; } = null;
        public DateTime? StartTime { get; set; } = null;
        public DateTime? EndTime { get; set; } = null;
        public EntityChangeType? ChangeType { get; set; } = null;
        public string EntityId { get; set; } = null;
        public string EntityTypeFullName { get; set; } = null;
        public bool IncludeDetails { get; set; } = false;
        public CancellationToken CancellationToken { get; set; } = default;
    }
}
