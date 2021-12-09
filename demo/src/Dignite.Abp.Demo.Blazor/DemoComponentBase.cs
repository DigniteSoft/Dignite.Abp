using Dignite.Abp.Demo.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Dignite.Abp.Demo.Blazor
{
    public abstract class DemoComponentBase : AbpComponentBase
    {
        protected DemoComponentBase()
        {
            LocalizationResource = typeof(DemoResource);
        }
    }
}
