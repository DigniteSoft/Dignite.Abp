using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace Dignite.Abp.Identity
{
    [DependsOn(
      typeof(DigniteAbpIdentityApplicationContractsModule),
      typeof(AbpHttpClientModule))]
    public class DigniteAbpIdentityHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "DigniteAbpIdentity";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(DigniteAbpIdentityApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
