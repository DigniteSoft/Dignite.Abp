using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Dignite.Abp.SettingManagement.HttpApi
{
    [Area("SettingManagement")]
    [RemoteService(Name = SettingManagementRemoteServiceConsts.RemoteServiceName)]
    public abstract class SettingsControllerBase : AbpController, ISettingsAppService
    {
        protected ISettingsAppService SettingsAppService { get; set; }

        [HttpGet]
        public async Task<ListResultDto<SettingNavigationDto>> GetAllAsync()
        {
            return await SettingsAppService.GetAllAsync();
        }

        [HttpPut]
        public async Task UpdateAsync( UpdateSettingsInput input)
        {
            await SettingsAppService.UpdateAsync(input);
        }
    }
}
