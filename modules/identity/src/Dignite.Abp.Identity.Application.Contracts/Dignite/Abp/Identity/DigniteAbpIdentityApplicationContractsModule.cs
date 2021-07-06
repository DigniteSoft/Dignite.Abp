using Dignite.Abp.Identity.Localization;
using Volo.Abp.Identity;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Dignite.Abp.Identity
{
    [DependsOn(
        typeof(AbpIdentityApplicationContractsModule)
        )]
    public class DigniteAbpIdentityApplicationContractsModule : AbpModule
    {

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<DigniteAbpIdentityApplicationContractsModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<IdentityOrganizationUnitResource>("en")
                    .AddBaseTypes(
                        typeof(Volo.Abp.Identity.Localization.IdentityResource)
                    ).AddVirtualJson("/Dignite/Abp/Identity/Localization");
            });

            /* 抛出商业异常时需要开启这里的代码
            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("Volo.Abp.Identity", typeof(IdentityResource));
            });
            */
        }
    }
}
