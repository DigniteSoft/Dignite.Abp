using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.NotificationCenter
{
    [Dependency(ReplaceServices = true)]
    public class NotificationCenterBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "NotificationCenter";
    }
}
