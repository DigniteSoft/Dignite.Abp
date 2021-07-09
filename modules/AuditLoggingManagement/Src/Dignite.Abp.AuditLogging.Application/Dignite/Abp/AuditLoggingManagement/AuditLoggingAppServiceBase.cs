using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Services;

namespace Dignite.Abp.AuditLoggingManagement.Application.Dignite.Abp.AuditLoggingManagement
{
    public class AuditLoggingAppServiceBase : ApplicationService
    {
        protected AuditLoggingAppServiceBase()
        {
            ObjectMapperContext = typeof(DigniteAbpAuditLoggingApplicationModule);
        }
    }
}
