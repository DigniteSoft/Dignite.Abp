using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Dignite.Abp.Identity;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(DigniteAbpIdentityHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class IdentityConsoleApiClientModule : AbpModule
{

}
