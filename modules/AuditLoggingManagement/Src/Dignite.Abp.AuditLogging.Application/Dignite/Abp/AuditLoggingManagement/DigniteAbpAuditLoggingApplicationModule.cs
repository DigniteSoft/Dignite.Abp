using Dignite.Abp.AuditLoggingManagement.Application.Contracts.Dignite.Abp.AuditLoggingManagement;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.AuditLogging;
using Volo.Abp.Modularity;

namespace Dignite.Abp.AuditLoggingManagement.Application.Dignite.Abp.AuditLoggingManagement
{
    [DependsOn(
        typeof(AbpAuditLoggingDomainModule),
        typeof(DigniteAbpAuditLoggingApplicationContractModule)
     )]
    public class DigniteAbpAuditLoggingApplicationModule : AbpModule
    {

    }
}
