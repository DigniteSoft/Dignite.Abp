using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace Dignite.Abp.Identity
{
    [RemoteService(Name = Volo.Abp.Identity.IdentityRemoteServiceConsts.RemoteServiceName)]
    [Area(IdentityRemoteServiceConsts.ModuleName)]
    [ControllerName("OrganizationUnit")]
    [Route("api/identity/organization-units")]
    public class OrganizationUnitController : DigniteAbpIdentityController, IOrganizationUnitAppService
    {
        private readonly IOrganizationUnitAppService _organizationUnitAppService;
        public OrganizationUnitController(IOrganizationUnitAppService organizationUnitAppService)
        {
            _organizationUnitAppService = organizationUnitAppService;
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
        public async Task<OrganizationUnitDto> MoveAsync(Guid id, OrganizationUnitMoveInput input)
        {
            return await _organizationUnitAppService.MoveAsync(id, input);
        }

        [HttpGet]
        [Route("{id}/roles")]
        public async Task<ListResultDto<IdentityRoleDto>> GetRolesAsync(Guid id)
        {
            return await _organizationUnitAppService.GetRolesAsync(id);
        }

        [HttpGet]
        [Route("available-roles")]
        public async Task<ListResultDto<IdentityRoleDto>> GetAvailableRolesAsync(Guid? parentId)
        {
            return await _organizationUnitAppService.GetAvailableRolesAsync(parentId);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<OrganizationUnitDto> GetAsync(Guid id)
        {
            return await _organizationUnitAppService.GetAsync(id);
        }


        [HttpGet]
        [Route("by-code/{code}")]
        public async Task<OrganizationUnitDto> FindByCodeAsync(string code)
        {
            return await _organizationUnitAppService.FindByCodeAsync(code);
        }

        [HttpGet]
        [Route("authorized")]
        public async Task<ListResultDto<OrganizationUnitDto>> GetAuthorizedAsync(bool buildOrganizationUnitsTree = false)
        {
            return await _organizationUnitAppService.GetAuthorizedAsync(buildOrganizationUnitsTree);
        }

        [HttpGet]
        public async Task<PagedResultDto<OrganizationUnitDto>> GetListAsync(GetOrganizationUnitsInput input)
        {
            return await _organizationUnitAppService.GetListAsync(input);
        }

        [HttpPost]
        [Route("{id}/members")]
        public async Task AddMembersAsync(Guid id, Guid[] userIds)
        {
            await _organizationUnitAppService.AddMembersAsync(id, userIds);
        }


        [HttpDelete]
        [Route("{id}/members")]
        public async Task RemoveMembersAsync(Guid id, Guid[] userIds)
        {
            await _organizationUnitAppService.RemoveMembersAsync(id, userIds);
        }

        [HttpGet]
        [Route("{id}/members")]
        public async Task<PagedResultDto<IdentityUserDto>> GetMembersAsync(Guid id, GetOrganizationUnitMembersInput input)
        {
            return await _organizationUnitAppService.GetMembersAsync(id, input);
        }

    }
}
