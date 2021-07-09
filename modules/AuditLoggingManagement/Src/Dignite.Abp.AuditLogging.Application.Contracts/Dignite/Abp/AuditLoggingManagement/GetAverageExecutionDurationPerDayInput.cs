using System;
using System.Collections.Generic;
using System.Text;

namespace Dignite.Abp.AuditLoggingManagement.Application.Contracts.Dignite.Abp.AuditLoggingManagement
{
    public class GetAverageExecutionDurationPerDayInput
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
