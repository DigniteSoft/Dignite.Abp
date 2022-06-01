using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.BlazoriseUI;
using Volo.Abp.Modularity;

namespace Dignite.Abp.AntDesignBlazorUI
{
    [DependsOn(
        typeof(AbpBlazoriseUIModule)
        )]
    public class DigniteAbpAntDesignBlazorUIModule: AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAntDesign();
        }
    }
}
