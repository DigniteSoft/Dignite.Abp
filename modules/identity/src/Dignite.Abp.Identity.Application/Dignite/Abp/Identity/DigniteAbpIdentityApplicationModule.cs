using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
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
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<DigniteAbpIdentityApplicationModule>();

            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<DigniteAbpIdentityApplicationModuleAutoMapperProfile>(validate: true);
            });

            Configure<AuthorizationOptions>(options =>
            {
                options.AddPolicy("DigniteIdentityCreatePolicy", policy => policy.Requirements.Add(CommonOperations.Create));
                options.AddPolicy("DigniteIdentityUpdatePolicy", policy => policy.Requirements.Add(CommonOperations.Update));
                options.AddPolicy("DigniteIdentityDeletePolicy", policy => policy.Requirements.Add(CommonOperations.Delete));
            });

            context.Services.AddSingleton<IAuthorizationHandler, OrganizationAuthorizationHandler>();
        }
    }
}