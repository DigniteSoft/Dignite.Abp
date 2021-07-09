using AutoMapper;
using Dignite.Abp.AuditLoggingManagement.Application.Contracts.Dignite.Abp.AuditLoggingManagement;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.AuditLogging;

namespace Dignite.Abp.AuditLoggingManagement.Application.Dignite.Abp.AuditLoggingManagement
{
    public class DigniteAbpAuditLoggingApplicationModuleAutoMapperProfile : Profile
    {
        public DigniteAbpAuditLoggingApplicationModuleAutoMapperProfile()
        {
            CreateMap<AuditLog, AuditLogDto>()
                 .ForMember(t => t.EntityChanges, option => option.MapFrom(l => l.EntityChanges))
                 .ForMember(t => t.Actions, option => option.MapFrom(l => l.Actions));
            CreateMap<EntityChange, EntityChangeDto>()
                 .ForMember(t => t.PropertyChanges, option => option.MapFrom(l => l.PropertyChanges));
            CreateMap<AuditLogAction, AuditLogActionDto>();
            CreateMap<EntityPropertyChange, EntityPropertyChangeDto>();
        }
    }
}
