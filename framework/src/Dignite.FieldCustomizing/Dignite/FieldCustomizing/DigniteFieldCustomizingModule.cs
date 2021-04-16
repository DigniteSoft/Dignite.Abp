
using Dignite.FieldCustomizing.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.Threading;

namespace Dignite.FieldCustomizing
{
    [DependsOn(
        typeof(AbpLocalizationModule),
        typeof(AbpThreadingModule)
        )]
    public class DigniteFieldCustomizingModule:AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<FieldCustomizingResource>("en")
                    .AddVirtualJson("/Dignite/FieldCustomizing/Localization");
            });
        }
    }
}
