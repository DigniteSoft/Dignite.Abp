﻿using AutoMapper;

namespace Dignite.Abp.FileManagement
{
    public class FileManagementApplicationAutoMapperProfile : Profile
    {
        public FileManagementApplicationAutoMapperProfile()
        {
            CreateMap<File, FileDto>();
        }
    }
}