using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Packages.Bootstrap;
using Volo.Abp.AspNetCore.Mvc.UI.Packages.JQuery;
using Volo.Abp.AspNetCore.Mvc.UI.Packages.JQueryForm;
using Volo.Abp.AspNetCore.Mvc.UI.Packages.JQueryValidationUnobtrusive;
using Volo.Abp.AspNetCore.Mvc.UI.Packages.Lodash;
using Volo.Abp.AspNetCore.Mvc.UI.Packages.Luxon;
using Volo.Abp.AspNetCore.Mvc.UI.Packages.MalihuCustomScrollbar;
using Volo.Abp.AspNetCore.Mvc.UI.Packages.SweetAlert;
using Volo.Abp.AspNetCore.Mvc.UI.Packages.Timeago;
using Volo.Abp.AspNetCore.Mvc.UI.Packages.Toastr;
using Volo.Abp.Modularity;

namespace Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Bundling
{
    [DependsOn(
        typeof(JQueryScriptContributor),
        typeof(BootstrapScriptContributor),
        typeof(LodashScriptContributor),
        typeof(JQueryValidationUnobtrusiveScriptContributor),
        typeof(JQueryFormScriptContributor),
        typeof(SweetalertScriptContributor),
        typeof(ToastrScriptBundleContributor),
        typeof(MalihuCustomScrollbarPluginScriptBundleContributor),
        typeof(LuxonScriptContributor),
        typeof(TimeagoScriptContributor)
        )]
    public class PureThemeGlobalScriptContributor : BundleContributor
    {
        public override void ConfigureBundle(BundleConfigurationContext context)
        {
            context.Files.AddRange(new[]
            {
                "/libs/abp/aspnetcore-mvc-ui-theme-shared/jquery/jquery-extensions.js",
                "/libs/abp/aspnetcore-mvc-ui-theme-shared/jquery-form/jquery-form-extensions.js",
                "/libs/abp/aspnetcore-mvc-ui-theme-shared/jquery/widget-manager.js",
                "/libs/abp/aspnetcore-mvc-ui-theme-shared/bootstrap/modal-manager.js",
                "/libs/abp/aspnetcore-mvc-ui-theme-shared/sweetalert/abp-sweetalert.js",
                "/libs/abp/aspnetcore-mvc-ui-theme-shared/toastr/abp-toastr.js",
                "/themes/pure/layout.js"
            });
        }
    }
}