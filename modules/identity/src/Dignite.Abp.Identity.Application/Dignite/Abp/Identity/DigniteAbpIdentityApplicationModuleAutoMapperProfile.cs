using AutoMapper;
using Volo.Abp.Identity;

namespace Dignite.Abp.Identity
{
    public class DigniteAbpIdentityApplicationModuleAutoMapperProfile : Profile
    {
        public DigniteAbpIdentityApplicationModuleAutoMapperProfile()
        {
            CreateMap<IdentityRole, Volo.Abp.Identity.IdentityRoleDto>()
                .MapExtraProperties(definitionChecks: Volo.Abp.ObjectExtending.MappingPropertyDefinitionChecks.Source);

            CreateMap<IdentityRole, IdentityRoleDto>()
                .ForMember(r => r.ParentId, r => r.Ignore())
                .ForMember(r => r.Children, r => r.Ignore())
                .MapExtraProperties(definitionChecks: Volo.Abp.ObjectExtending.MappingPropertyDefinitionChecks.Source);

            CreateMap<OrganizationUnit, OrganizationUnitDto>()
                .ForMember(r => r.IsActive, r => r.Ignore())
                .ForMember(r => r.Sort, r => r.Ignore())
                .ForMember(r => r.Children, r => r.Ignore())
                .ForMember(r => r.HasChild, r => r.Ignore())
                .MapExtraProperties(definitionChecks: Volo.Abp.ObjectExtending.MappingPropertyDefinitionChecks.Source);

            CreateMap<OrganizationUnitRole, OrganizationUnitRoleDto>();
        }
    }
}
