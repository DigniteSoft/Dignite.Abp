using Dignite.Abp.BlobStoringManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Dignite.Abp.BlobStoringManagement
{
    public abstract class BlobStoringManagementController : AbpController
    {
        protected BlobStoringManagementController()
        {
            LocalizationResource = typeof(BlobStoringManagementResource);
        }
    }
}
