using Volo.Abp.Modularity;

namespace Dignite.Abp.Identity;

[DependsOn(
    typeof(DigniteAbpIdentityApplicationModule)
    )]
public class IdentityApplicationTestModule : AbpModule
{

}
