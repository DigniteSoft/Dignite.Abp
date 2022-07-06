using System.Collections.Generic;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace Dignite.Abp.Identity.Blazor.Server
{
    public class BlazorDigniteAbpIdentityStyleContributor : BundleContributor
    {
        public override void ConfigureBundle(BundleConfigurationContext context)
        {
            context.Files.AddIfNotContains("/_content/Blazorise.TreeView/blazorise.treeview.css");
        }
    }
}
