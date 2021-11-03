using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Identity;

namespace Dignite.Abp.Identity
{
    [RemoteService]
    [Route("api/identity/organization-unit")]
    public class OrganizationUnitController : DigniteAbpIdentityController, IOrganizationUnitAppService
    {
        private readonly IOrganizationUnitAppService _organizationUnitAppService;
        public OrganizationUnitController(IOrganizationUnitAppService organizationUnitAppService)
        {
            _organizationUnitAppService = organizationUnitAppService;
        }

        [HttpGet]
        [Route("new")]
        public async Task<GetOrganizationUnitForEditOutput> NewAsync(Guid? parentId)
        {
            return await _organizationUnitAppService.NewAsync(parentId);
        }

        [HttpGet]
        [Route("{id}/edit")]
        public Task<GetOrganizationUnitForEditOutput> GetOrganizationUnitForEditAsync(Guid id)
        {
            return _organizationUnitAppService.GetOrganizationUnitForEditAsync(id);
        }

        [HttpPost]
        public async Task<OrganizationUnitDto> CreateAsync(OrganizationUnitCreateDto input)
        {
            return await _organizationUnitAppService.CreateAsync(input);
        }

        [HttpPut]
        public async Task<OrganizationUnitDto> UpdateAsync(Guid id, OrganizationUnitUpdateDto input)
        {
            return await _organizationUnitAppService.UpdateAsync(id, input);
        }


        [HttpDelete]
        public async Task DeleteAsync(Guid id)
        {
            await _organizationUnitAppService.DeleteAsync(id);
        }

        [HttpPut]
        [Route("{id}/move")]
        public async Task MoveAsync(Guid id, OrganizationUnitMoveInput input)
        {
            await _organizationUnitAppService.MoveAsync(id, input);
        }

        [HttpGet]
        [Route("{id}/roles")]
        public async Task<ListResultDto<IdentityRoleDto>> GetRolesAsync(Guid id)
        {
            return await _organizationUnitAppService.GetRolesAsync(id);
        }

        [HttpGet]
        public async Task<ListResultDto<OrganizationUnitDto>> SearchAsync(string filter)
        {
            return await _organizationUnitAppService.SearchAsync(filter);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<OrganizationUnitDto> GetAsync(Guid id)
        {
            return await _organizationUnitAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("{id}/parents")]
        public async Task<ListResultDto<OrganizationUnitDto>> GetParentsAsync(Guid id)
        {
            return await _organizationUnitAppService.GetParentsAsync(id);
        }


        [HttpGet]
        [Route("by-code/{code}")]
        public async Task<OrganizationUnitDto> FindByCodeAsync(string code)
        {
            return await _organizationUnitAppService.FindByCodeAsync(code);
        }

        [HttpGet]
        [Route("authorized")]
        public async Task<ListResultDto<OrganizationUnitDto>> GetAuthorizedAsync()
        {
            return await _organizationUnitAppService.GetAuthorizedAsync();
        }

        [HttpGet]
        [Route("children")]
        public async Task<ListResultDto<OrganizationUnitDto>> GetChildrenAsync(Guid? parentId, bool recursive = false)
        {
            return await _organizationUnitAppService.GetChildrenAsync(parentId, recursive);
        }

        [HttpPost]
        [Route("{id}/member")]
        public async Task AddMembersAsync(Guid id, OrganizationUnitAddMembersInput input)
        {
            await _organizationUnitAppService.AddMembersAsync(id, input);
        }

        [HttpPut]
        [Route("{id}/member")]
        public async Task UpdateMemberRolesAsync(Guid id, Guid userId, OrganizationUnitUpdateMemberRolesInput input)
        {
            await _organizationUnitAppService.UpdateMemberRolesAsync(id, userId, input);
        }

        [HttpDelete]
        [Route("{id}/member")]
        public async Task RemoveMembersAsync(Guid id, OrganizationUnitRemoveMembersInput input)
        {
            await _organizationUnitAppService.RemoveMembersAsync(id, input);
        }

        [HttpGet]
        [Route("{id}/members")]
        public async Task<PagedResultDto<IdentityUserDto>> GetMembersAsync(Guid id, GetOrganizationUnitMembersInput input)
        {
            return await _organizationUnitAppService.GetMembersAsync(id, input);
        }


        [HttpGet]
        [Route("{id}/{userId}/assignable-roles")]
        public async Task<ListResultDto<IdentityRoleDto>> GetMemberAssignableRolesAsync(Guid id, Guid userId)
        {
            return await _organizationUnitAppService.GetMemberAssignableRolesAsync(id, userId);
        }
    }
}
