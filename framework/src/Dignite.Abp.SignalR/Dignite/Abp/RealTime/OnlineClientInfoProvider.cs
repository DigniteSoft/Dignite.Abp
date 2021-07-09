using Dignite.Abp.SignalR.Dignite.Abp.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using Volo.Abp.AspNetCore.WebClientInfo;

namespace Dignite.Abp.RealTime
{
    public class OnlineClientInfoProvider : IOnlineClientInfoProvider
    {

        private readonly IWebClientInfoProvider _clientInfoProvider;

        public OnlineClientInfoProvider(IWebClientInfoProvider clientInfoProvider, ILogger<OnlineClientInfoProvider> logger)
        {
            _clientInfoProvider = clientInfoProvider;
            Logger = logger;
        }

        public ILogger Logger { get; set; }

        public IOnlineClient CreateClientForCurrentConnection(HubCallerContext context)
        {
            return new OnlineClient(
                context.ConnectionId,
                GetIpAddressOfClient(context),
                context.GetTenantId(),
                context.GetUserIdOrNull()
            );
        }

        private string GetIpAddressOfClient(HubCallerContext context)
        {
            try
            {
                return _clientInfoProvider.ClientIpAddress;
            }
            catch (Exception ex)
            {
                Logger.LogError("Can not find IP address of the client! connectionId: " + context.ConnectionId);
                Logger.LogError(ex.Message, ex);
                return "";
            }
        }
    }
}
