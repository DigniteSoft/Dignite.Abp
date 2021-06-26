using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Identity.Localization;

namespace Dignite.Abp.Identity
{
    public abstract class DigniteAbpIdentityController : AbpController
    {
        protected DigniteAbpIdentityController()
        {
            LocalizationResource = typeof(IdentityResource);
        }
    }
}
