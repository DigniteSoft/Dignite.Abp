using Volo.Abp;
using Volo.Abp.Testing;

namespace Dignite.Abp.Settings
{
    public class SettingsTestBase : AbpIntegratedTest<AbpSettingsTestModule>
    {
        protected override void SetAbpApplicationCreationOptions(AbpApplicationCreationOptions options)
        {
            options.UseAutofac();
        }

    }
}