using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Dignite.Abp.NotificationCenter.Blazor.Server.Host
{
    [Dependency(ReplaceServices = true)]
    public class NotificationCenterBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "NotificationCenter";
    }
}
