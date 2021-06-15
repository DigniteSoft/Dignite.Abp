using Dignite.Abp.Notifications;
using Dignite.Abp.Notifications.RealTime;
using Dignite.Abp.SignalR.Dignite.Abp.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.SignalR.Dignite.Abp.Notifications
{
    public class SignalRRealTimeNotifier : IRealTimeNotifier, ITransientDependency
    {
        public ILogger Logger { get; set; }

        private readonly IOnlineClientManager _onlineClientManager;

        private readonly IHubContext<AbpCommonHub> _hubContext;

        public SignalRRealTimeNotifier(
        IOnlineClientManager onlineClientManager,
        IHubContext<AbpCommonHub> hubContext)
        {
            _onlineClientManager = onlineClientManager;
            _hubContext = hubContext;
            Logger = NullLogger.Instance;
        }


        public async Task SendNotificationsAsync(UserNotification[] userNotifications)
        {
            foreach (var userNotification in userNotifications)
            {
                try
                {
                    var onlineClients = _onlineClientManager.GetAllByUserId(userNotification);
                    foreach (var onlineClient in onlineClients)
                    {
                        var signalRClient = _hubContext.Clients.Client(onlineClient.ConnectionId);
                        if (signalRClient == null)
                        {
                            Logger.LogDebug("Can not get user " + new UserIdentifier(userNotification.TenantId, userNotification.UserId) + " with connectionId " + onlineClient.ConnectionId + " from SignalR hub!");
                            continue;
                        }

                        userNotification.Notification.EntityType = null; // Serialization of System.Type causes SignalR to disconnect. See https://github.com/aspnetboilerplate/aspnetboilerplate/issues/5230
                        await signalRClient.SendAsync("getNotification", userNotification);
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogWarning("Could not send notification to user: " + new UserIdentifier(userNotification.TenantId, userNotification.UserId));
                    Logger.LogWarning(ex.ToString(), ex);
                }
            }
        }
    }
}
