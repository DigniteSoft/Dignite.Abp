using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace Dignite.Abp.Identity
{
    [DependsOn(
        typeof(AbpIdentityApplicationModule),
        typeof(DigniteAbpIdentityApplicationContractsModule)
        )]
    public class DigniteAbpIdentityApplicationModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            IdentityRoleExtensions.Configure();
            OrganizationUnitExtensions.Configure();
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<DigniteAbpIdentityApplicationModule>();

            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<DigniteAbpIdentityApplicationModuleAutoMapperProfile>(validate: true);
            });

            Configure<AbpDistributedEntityEventOptions>(options =>
            {
                options.AutoEventSelectors.Add<OrganizationUnit>();
            });

            Configure<AuthorizationOptions>(options =>
            {
                options.AddPolicy("DigniteIdentityCreatePolicy", policy => policy.Requirements.Add(CommonOperations.Create));
                options.AddPolicy("DigniteIdentityUpdatePolicy", policy => policy.Requirements.Add(CommonOperations.Update));
                options.AddPolicy("DigniteIdentityDeletePolicy", policy => policy.Requirements.Add(CommonOperations.Delete));
                options.AddPolicy("DigniteIdentityGetPolicy", policy => policy.Requirements.Add(CommonOperations.Get));
            });

            context.Services.AddSingleton<IAuthorizationHandler, OrganizationAuthorizationHandler>();
        }
    }
}