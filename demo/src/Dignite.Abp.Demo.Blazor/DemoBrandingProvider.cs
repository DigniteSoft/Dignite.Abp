using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Dignite.Abp.Demo.Blazor
{
    [Dependency(ReplaceServices = true)]
    public class DemoBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "Demo";
    }
}
