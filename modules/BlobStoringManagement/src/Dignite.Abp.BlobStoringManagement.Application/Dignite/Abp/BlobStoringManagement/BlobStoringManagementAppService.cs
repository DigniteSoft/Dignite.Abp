using Dignite.Abp.BlobStoringManagement.Localization;
using Volo.Abp.Application.Services;

namespace Dignite.Abp.BlobStoringManagement
{
    public abstract class BlobStoringManagementAppService : ApplicationService
    {
        protected BlobStoringManagementAppService()
        {
            LocalizationResource = typeof(BlobStoringManagementResource);
            ObjectMapperContext = typeof(BlobStoringManagementApplicationModule);
        }
    }
}
