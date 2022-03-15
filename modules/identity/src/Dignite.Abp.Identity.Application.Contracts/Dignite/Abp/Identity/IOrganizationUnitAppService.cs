using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;

namespace Dignite.Abp.Identity
{
    public interface IOrganizationUnitAppService: IApplicationService
    {
        Task<OrganizationUnitDto> GetAsync(Guid id);

        Task<ListResultDto<OrganizationUnitDto>> GetChildrenAsync(Guid? parentId, bool recursive = false);

        Task<ListResultDto<OrganizationUnitDto>> SearchAsync(string filter);

        Task<ListResultDto<OrganizationUnitDto>> GetParentsAsync(Guid id);

        /// <summary>
        /// 获取授权给当前用户的组织，即包含当前用户的组织；
        /// 如果当前用户拥有<see cref="OrganizationUnitPermissions.OrganizationUnits.SuperAuthorization"/>权限，则获取所有组织；
        /// 返回的结果中不包括授权组织单元下的子组织机构；
        /// </summary>
        /// <returns></returns>
        Task<ListResultDto<OrganizationUnitDto>> GetAuthorizedAsync();

        Task<GetOrganizationUnitForEditOutput> NewAsync(Guid? parentId);

        Task<GetOrganizationUnitForEditOutput> GetOrganizationUnitForEditAsync(Guid id);

        Task<OrganizationUnitDto> CreateAsync(OrganizationUnitCreateDto input);

        Task<OrganizationUnitDto> UpdateAsync(Guid id, OrganizationUnitUpdateDto input);

        Task DeleteAsync(Guid id);

        Task MoveAsync(Guid id, OrganizationUnitMoveInput input);

        Task<ListResultDto<IdentityRoleDto>> GetRolesAsync(Guid id);

        Task<PagedResultDto<IdentityUserDto>> GetMembersAsync(Guid id, GetOrganizationUnitMembersInput input);

        Task AddMembersAsync(Guid id, OrganizationUnitAddMembersInput input);

        Task RemoveMembersAsync(Guid id, OrganizationUnitRemoveMembersInput input);

        /// <summary>
        /// 获取可分配的角色，用于更新用户角色
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<ListResultDto<IdentityRoleDto>> GetMemberAssignableRolesAsync(Guid id,Guid userId);

        Task UpdateMemberRolesAsync(Guid id, Guid userId, OrganizationUnitUpdateMemberRolesInput input);


        Task<OrganizationUnitDto> FindByCodeAsync(string code);
    }
}
