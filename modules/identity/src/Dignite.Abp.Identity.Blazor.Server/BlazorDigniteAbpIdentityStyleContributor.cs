using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace Dignite.Abp.Identity.Blazor.Server
{
    public class BlazorDigniteAbpIdentityStyleContributor : BundleContributor
    {
        public override void ConfigureBundle(BundleConfigurationContext context)
        {
            context.Files.Add("/_content/AntDesign/css/ant-design-blazor.css");
        }
    }
}
