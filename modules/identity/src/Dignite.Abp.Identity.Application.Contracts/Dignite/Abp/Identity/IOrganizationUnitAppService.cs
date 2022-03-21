using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;

namespace Dignite.Abp.Identity
{
    public interface IOrganizationUnitAppService
    : ICrudAppService<
        OrganizationUnitDto,
        Guid,
        GetOrganizationUnitsInput,
        OrganizationUnitCreateDto,
        OrganizationUnitUpdateDto>
    {
        Task<OrganizationUnitDto> FindByCodeAsync(string code);

        /// <summary>
        /// 获取授权给当前用户的机构；
        /// 如果当前用户拥有<see cref="OrganizationUnitPermissions.OrganizationUnits.SuperAuthorization"/>权限，则获取所有组织；
        /// 返回的结果中不包括授权组织单元下的子组织机构；
        /// </summary>
        /// <param name="buildOrganizationUnitsTree">构建组织机构树</param>
        /// <returns></returns>
        Task<ListResultDto<OrganizationUnitDto>> GetAuthorizedAsync(bool buildOrganizationUnitsTree=false);

        Task<OrganizationUnitDto> MoveAsync(Guid id, OrganizationUnitMoveInput input);

        Task<ListResultDto<IdentityRoleDto>> GetRolesAsync(Guid id);

        Task<ListResultDto<IdentityRoleDto>> GetAvailableRolesAsync(Guid? parentId);

        Task<PagedResultDto<IdentityUserDto>> GetMembersAsync(Guid id, GetOrganizationUnitMembersInput input);

        Task AddMembersAsync(Guid id, Guid[] userIds);

        Task RemoveMembersAsync(Guid id, Guid[] userIds);
    }
}
