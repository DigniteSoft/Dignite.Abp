
using Volo.Abp.Bundling;

namespace Dignite.Abp.AntDesignBlazorUI.WebAssembly
{

    public class AntDesignBlazorBundleContributor : IBundleContributor
    {
        public void AddScripts(BundleContext context)
        {
            context.Add("_content/AntDesign/js/ant-design-blazor.js");
            context.Add("_content/Dignite.Abp.AntDesignBlazorUI/libs/abp/js/ant-design.js");
        }

        public void AddStyles(BundleContext context)
        {
            context.Add("_content/AntDesign/css/ant-design-blazor.css");
            context.Add("_content/Dignite.Abp.AntDesignBlazorUI/libs/abp/css/ant-design.css");
        }
    }
}
