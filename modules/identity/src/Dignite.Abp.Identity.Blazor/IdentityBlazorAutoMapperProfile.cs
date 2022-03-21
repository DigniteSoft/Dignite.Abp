using AutoMapper;

namespace Dignite.Abp.Identity.Blazor;

public class IdentityBlazorAutoMapperProfile : Profile
{
    public IdentityBlazorAutoMapperProfile()
    {
        CreateMap<OrganizationUnitDto, OrganizationUnitUpdateDto>()
                .ForMember(r => r.RoleIds, r => r.Ignore());
    }
}
