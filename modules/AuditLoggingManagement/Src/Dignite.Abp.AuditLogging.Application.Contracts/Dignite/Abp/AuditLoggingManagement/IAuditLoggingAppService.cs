using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Dignite.Abp.AuditLoggingManagement.Application.Contracts.Dignite.Abp.AuditLoggingManagement
{
    public interface IAuditLoggingAppService : IApplicationService
    {
        Task<PagedResultDto<AuditLogDto>> GetAuditLogsAsync(GetAuditLogsInput input);
        Task<AuditLogDto> GetAuditLogByIdAsync(Guid id);
        Task<GetAverageExecutionDurationPerDayOutput> GetAverageExecutionDurationPerDay(GetAverageExecutionDurationPerDayInput input);

    }
}
