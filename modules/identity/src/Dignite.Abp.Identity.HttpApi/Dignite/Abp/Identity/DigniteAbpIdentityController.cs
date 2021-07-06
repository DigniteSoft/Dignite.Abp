using Dignite.Abp.Identity.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Dignite.Abp.Identity
{
    public abstract class DigniteAbpIdentityController : AbpController
    {
        protected DigniteAbpIdentityController()
        {
            LocalizationResource = typeof(IdentityOrganizationUnitResource);
        }
    }
}
