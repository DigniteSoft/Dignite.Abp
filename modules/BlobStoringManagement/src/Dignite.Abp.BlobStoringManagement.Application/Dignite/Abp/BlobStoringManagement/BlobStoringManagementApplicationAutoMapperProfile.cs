using AutoMapper;

namespace Dignite.Abp.BlobStoringManagement
{
    public class BlobStoringManagementApplicationAutoMapperProfile : Profile
    {
        public BlobStoringManagementApplicationAutoMapperProfile()
        {
            CreateMap<Blob, BlobDto>();
        }
    }
}