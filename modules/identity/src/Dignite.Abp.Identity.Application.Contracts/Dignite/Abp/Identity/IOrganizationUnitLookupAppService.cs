using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Dignite.Abp.Identity;

public interface IOrganizationUnitLookupAppService : IApplicationService
{
    Task<OrganizationUnitDto> FindByIdAsync(Guid id);

    Task<OrganizationUnitDto> FindByCodeAsync(string code);

    Task<ListResultDto<OrganizationUnitDto>> GetChildrenAsync(GetOrganizationUnitsInput input);

    Task<ListResultDto<OrganizationUnitDto>> GetParentsAsync(Guid id);
}
