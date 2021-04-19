using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Dignite.Abp.SettingManagement
{
    public interface ISettingsAppService : IApplicationService
    {
        Task<ListResultDto<SettingNavigationDto>> GetNavigationsAsync();

        Task<ListResultDto<SettingGroupDto>> GetListAsync(string navigationName);

        Task UpdateAsync(UpdateSettingsInput input);
    }
}
