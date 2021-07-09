using Dignite.Abp.Notifications;
using Dignite.Abp.RealTime;
using Dignite.Abp.SignalR.Dignite.Abp.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.Notification.SignalRNotifier
{
    public class SignalRRealTimeNotifier : IRealTimeNotifier, ITransientDependency
    {
        public ILogger Logger { get; set; }

        private readonly IOnlineClientManager _onlineClientManager;

        private readonly IHubContext<AbpCommonHub> _hubContext;

        public SignalRRealTimeNotifier(
        IOnlineClientManager onlineClientManager,
        IHubContext<AbpCommonHub> hubContext,
        Logger<SignalRRealTimeNotifier> logger)
        {
            _onlineClientManager = onlineClientManager;
            _hubContext = hubContext;
            Logger = logger;
        }


        public async Task SendNotificationsAsync(UserNotificationInfo[] userNotifications)
        {
            foreach (var userNotification in userNotifications)
            {
                try
                {
                    var onlineClients = _onlineClientManager.GetAllByUserId(new UserIdentifier(userNotification.TenantId, userNotification.UserId));
                    foreach (var onlineClient in onlineClients)
                    {
                        var signalRClient = _hubContext.Clients.Client(onlineClient.ConnectionId);
                        if (signalRClient == null)
                        {
                            Logger.LogDebug("Can not get user " + userNotification.UserId + " with connectionId " + onlineClient.ConnectionId + " from SignalR hub!");
                            continue;
                        }

                        await signalRClient.SendAsync("getNotification", userNotification);
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogWarning("Could not send notification to user: " + userNotification.UserId);
                    Logger.LogWarning(ex.ToString(), ex);
                }
            }
        }
    }
}
