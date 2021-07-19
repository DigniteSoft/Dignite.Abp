using Dignite.Abp.Notifications;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.AspNetCore.SignalR.Notifications
{
    public class SignalRRealTimeNotifier : IRealTimeNotifier, ITransientDependency
    {
        public ILogger Logger { get; set; }

        private readonly IHubContext<NotificationHub,INotificationClient> _hubContext;

        public SignalRRealTimeNotifier(
        IHubContext<NotificationHub, INotificationClient> hubContext,
        Logger<SignalRRealTimeNotifier> logger)
        {
            _hubContext = hubContext;
            Logger = logger;
        }


        public async Task SendNotificationsAsync(UserNotificationInfo[] userNotifications)
        {
            await _hubContext.Clients.Users(
                userNotifications.Select(un => un.UserId.ToString())
                ).ReceiveNotifications();            
        }
    }
}
