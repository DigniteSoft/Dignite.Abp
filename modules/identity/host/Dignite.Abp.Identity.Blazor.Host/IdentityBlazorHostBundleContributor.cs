using Volo.Abp.Bundling;

namespace Dignite.Abp.Identity.Blazor.Host;

public class IdentityBlazorHostBundleContributor : IBundleContributor
{
    public void AddScripts(BundleContext context)
    {

    }

    public void AddStyles(BundleContext context)
    {
        context.Add("main.css", true);
    }
}
