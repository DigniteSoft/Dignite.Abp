using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Dignite.Abp.Identity
{
    [RemoteService(Name = Volo.Abp.Identity.IdentityRemoteServiceConsts.RemoteServiceName)]
    [Area(IdentityRemoteServiceConsts.ModuleName)]
    [ControllerName("OrganizationUnitLookup")]
    [Route("api/identity/organization-units/lookup")]
    public class OrganizationUnitLookupController : DigniteAbpIdentityController, IOrganizationUnitLookupAppService
    {
        private readonly IOrganizationUnitLookupAppService _organizationUnitAppService;

        public OrganizationUnitLookupController(IOrganizationUnitLookupAppService organizationUnitAppService)
        {
            _organizationUnitAppService = organizationUnitAppService;
        }

        [HttpGet]
        [Route("by-code/{code}")]
        public async Task<OrganizationUnitDto> FindByCodeAsync(string code)
        {
            return await _organizationUnitAppService.FindByCodeAsync(code);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<OrganizationUnitDto> FindByIdAsync(Guid id)
        {
            return await _organizationUnitAppService.FindByIdAsync(id);
        }

        [HttpGet]
        [Route("children")]
        public async Task<ListResultDto<OrganizationUnitDto>> GetChildrenAsync(GetOrganizationUnitsInput input)
        {
            return await _organizationUnitAppService.GetChildrenAsync(input);
        }

        [HttpGet]
        [Route("{id:guid}/parents")]
        public async Task<ListResultDto<OrganizationUnitDto>> GetParentsAsync(Guid id)
        {
            return await _organizationUnitAppService.GetParentsAsync(id);
        }
    }
}
