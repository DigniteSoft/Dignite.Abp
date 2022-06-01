using Dignite.Abp.Identity.Localization;
using Volo.Abp.Application.Services;

namespace Dignite.Abp.Identity
{
    public abstract class IdentityAppServiceBase : ApplicationService
    {
        protected IdentityAppServiceBase()
        {
            ObjectMapperContext = typeof(DigniteAbpIdentityApplicationModule);
            LocalizationResource = typeof(DigniteAbpIdentityResource);
        }
    }
}
