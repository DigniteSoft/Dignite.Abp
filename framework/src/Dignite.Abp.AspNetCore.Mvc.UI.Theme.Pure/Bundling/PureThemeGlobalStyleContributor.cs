using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Packages.Bootstrap;
using Volo.Abp.AspNetCore.Mvc.UI.Packages.Core;
using Volo.Abp.AspNetCore.Mvc.UI.Packages.FontAwesome;
using Volo.Abp.AspNetCore.Mvc.UI.Packages.MalihuCustomScrollbar;
using Volo.Abp.AspNetCore.Mvc.UI.Packages.Toastr;
using Volo.Abp.Modularity;

namespace Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Bundling
{
    [DependsOn(
        typeof(CoreStyleContributor),
        typeof(BootstrapStyleContributor),
        typeof(FontAwesomeStyleContributor),
        typeof(ToastrStyleBundleContributor),
        typeof(MalihuCustomScrollbarPluginStyleBundleContributor)
    )]
    public class PureThemeGlobalStyleContributor : BundleContributor
    {
        public override void ConfigureBundle(BundleConfigurationContext context)
        {
            context.Files.Add("/themes/pure/layout.css");
        }
    }
}
