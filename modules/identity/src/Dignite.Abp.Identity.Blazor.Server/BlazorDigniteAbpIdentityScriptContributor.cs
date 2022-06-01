using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace Dignite.Abp.Identity.Blazor.Server
{
    public class BlazorDigniteAbpIdentityScriptContributor: BundleContributor
    {
        public override void ConfigureBundle(BundleConfigurationContext context)
        {
            context.Files.Add("/_content/AntDesign/js/ant-design-blazor.js");
        }
    }
}
