using AutoMapper;

namespace Dignite.Abp.Notifications
{
    public class NotificationsApplicationAutoMapperProfile : Profile
    {
        public NotificationsApplicationAutoMapperProfile()
        {
            CreateMap<NotificationSubscriptionInfo, NotificationSubscriptionDto>()
                .ForMember(m => m.DisplayName, y => y.Ignore())
                .ForMember(m => m.Description, y => y.Ignore());
        }
    }
}