using AutoMapper;
using Volo.Abp.Identity;

namespace Dignite.Abp.Identity
{
    public class DigniteAbpIdentityApplicationModuleAutoMapperProfile : Profile
    {
        public DigniteAbpIdentityApplicationModuleAutoMapperProfile()
        {
            CreateMap<IdentityRole, Volo.Abp.Identity.IdentityRoleDto>()
                .MapExtraProperties(definitionChecks: Volo.Abp.ObjectExtending.MappingPropertyDefinitionChecks.None);

            CreateMap<IdentityRole, IdentityRoleDto>()
                .ForMember(r => r.ParentId, r => r.Ignore())
                .MapExtraProperties(definitionChecks: Volo.Abp.ObjectExtending.MappingPropertyDefinitionChecks.None);

            CreateMap<OrganizationUnit, OrganizationUnitDto>()
                .ForMember(r => r.IsActive, r => r.Ignore())
                .ForMember(r => r.Position, r => r.Ignore())
                .ForMember(r => r.Children, r => r.Ignore())
                .ForMember(r => r.ChildrenCount, r => r.Ignore())
                .MapExtraProperties();
            CreateMap<OrganizationUnit, OrganizationUnitEditDto>()
                .ForMember(r => r.IsActive, r => r.Ignore())
                .MapExtraProperties();
        }
    }
}
