
using Volo.Abp.Bundling;

namespace Dignite.Abp.Identity.Blazor.WebAssembly
{

    public class AntDesignBlazorBundleContributor : IBundleContributor
    {
        public void AddScripts(BundleContext context)
        {
            context.Add("_content/AntDesign/js/ant-design-blazor.js");
        }

        public void AddStyles(BundleContext context)
        {
            context.Add("_content/AntDesign/css/ant-design-blazor.css");
        }
    }
}
