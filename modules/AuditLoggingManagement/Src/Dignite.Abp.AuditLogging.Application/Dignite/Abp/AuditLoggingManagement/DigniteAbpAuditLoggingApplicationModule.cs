using Dignite.Abp.AuditLoggingManagement.Application.Contracts.Dignite.Abp.AuditLoggingManagement;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.AuditLogging;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace Dignite.Abp.AuditLoggingManagement.Application.Dignite.Abp.AuditLoggingManagement
{
    [DependsOn(
        typeof(AbpAuditLoggingDomainModule),
        typeof(DigniteAbpAuditLoggingApplicationContractModule)
     )]
    public class DigniteAbpAuditLoggingApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<DigniteAbpAuditLoggingApplicationModule>();

            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<DigniteAbpAuditLoggingApplicationModuleAutoMapperProfile>(validate: true);
            });
        }
    }
}
