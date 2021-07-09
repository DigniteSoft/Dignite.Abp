using System;
using System.Collections.Generic;
using System.Text;

namespace Dignite.Abp.AuditLoggingManagement.Application.Contracts.Dignite.Abp.AuditLoggingManagement
{
    public class GetAverageExecutionDurationPerDayOutput
    {
        public Dictionary<DateTime, double> Data { get; set; }
    }
}
