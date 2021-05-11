using Volo.Abp.Application.Services;
using Volo.Abp.Identity.Localization;

namespace Dignite.Abp.Identity
{
    public abstract class IdentityAppServiceBase : ApplicationService
    {
        protected IdentityAppServiceBase()
        {
            ObjectMapperContext = typeof(DigniteAbpIdentityApplicationModule);
            LocalizationResource = typeof(IdentityResource);
        }
    }
}
