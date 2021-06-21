using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application;
using Volo.Abp.AuditLogging;
using Volo.Abp.Modularity;

namespace Dignite.Abp.AuditLoggingManagement.Application.Contracts.Dignite.Abp.AuditLoggingManagement
{
    [DependsOn(
        typeof(AbpAuditLoggingDomainSharedModule),
        typeof(AbpDddApplicationModule)
       )]
    public class DigniteAbpAuditLoggingApplicationContractModule : AbpModule
    {
    }
}
