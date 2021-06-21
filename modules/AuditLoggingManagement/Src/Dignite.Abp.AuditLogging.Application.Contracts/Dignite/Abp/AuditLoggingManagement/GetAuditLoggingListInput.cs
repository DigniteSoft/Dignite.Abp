using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;

namespace Dignite.Abp.AuditLoggingManagement.Application.Contracts.Dignite.Abp.AuditLoggingManagement
{
    public class GetAuditLoggingListInput
    {
        public string Sorting { get; set; } = null;
        public int MaxResultCount { get; set; } = 50;
        public int SkipCount { get; set; } = 0;
        public DateTime? StartTime { get; set; } = null;
        public DateTime? EndTime { get; set; } = null;
        public string HttpMethod { get; set; } = null;
        public string Url { get; set; } = null;
        public string UserName { get; set; } = null;
        public string ApplicationName { get; set; } = null;
        public string CorrelationId { get; set; } = null;
        public int? MaxExecutionDuration { get; set; } = null;
        public int? MinExecutionDuration { get; set; } = null;
        public bool? HasException { get; set; } = null;
        public HttpStatusCode? HttpStatusCode { get; set; } = null;
        public bool IncludeDetails { get; set; } = false;
        public CancellationToken CancellationToken { get; set; } = default;
    }
}
