using Volo.Abp.Bundling;

namespace Dignite.Abp.NotificationCenter.Blazor.Host
{
    public class NotificationCenterBlazorHostBundleContributor : IBundleContributor
    {
        public void AddScripts(BundleContext context)
        {

        }

        public void AddStyles(BundleContext context)
        {
            context.Add("main.css", true);
        }
    }
}
