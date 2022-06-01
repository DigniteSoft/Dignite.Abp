using AutoMapper;
using Volo.Abp.AutoMapper;
using Volo.Abp.Identity;

namespace Dignite.Abp.Identity.Blazor;

public class IdentityBlazorAutoMapperProfile : Profile
{
    public IdentityBlazorAutoMapperProfile()
    {
        CreateMap<IdentityUserDto, IdentityUserUpdateDto>()
            .MapExtraProperties()
            .Ignore(x => x.Password)
            .Ignore(x => x.RoleNames);

        CreateMap<Volo.Abp.Identity.IdentityRoleDto, IdentityRoleUpdateDto>()
            .MapExtraProperties();

        CreateMap<OrganizationUnitDto, OrganizationUnitUpdateDto>()
                .ForMember(r => r.RoleIds, r => r.Ignore());
    }
}
