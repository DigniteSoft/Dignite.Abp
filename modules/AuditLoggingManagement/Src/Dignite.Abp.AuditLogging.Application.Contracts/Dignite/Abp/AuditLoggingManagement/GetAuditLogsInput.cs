using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using Volo.Abp.Application.Dtos;

namespace Dignite.Abp.AuditLoggingManagement.Application.Contracts.Dignite.Abp.AuditLoggingManagement
{
    public class GetAuditLogsInput : PagedResultRequestDto
    {
        public string Sorting { get; set; }

        public string Url { get; set; }

        public string UserName { get; set; }

        public string ApplicationName { get; set; }
        public string CorrelationId { get; set; }

        public string HttpMethod { get; set; }

        public HttpStatusCode? HttpStatusCode { get; set; }

        public int? MaxExecutionDuration { get; set; }


        public int? MinExecutionDuration { get; set; }

        public bool HasException { get; set; }
    }
}
