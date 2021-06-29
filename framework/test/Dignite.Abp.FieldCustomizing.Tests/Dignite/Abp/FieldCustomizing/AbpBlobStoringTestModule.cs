using Dignite.Abp.FieldCustomizing;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;


namespace Dignite.Abp.FieldCustomizing
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(DigniteAbpFieldCustomizingModule)
        )]
    public class AbpBlobStoringTestModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
        }
    }
}