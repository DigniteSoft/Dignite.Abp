using Dignite.Abp.RealTime;
using Microsoft.Extensions.Logging;

namespace Dignite.Abp.SignalR.Dignite.Abp.SignalR.Hubs
{
    public class AbpCommonHub : OnlineClientHubBase
    {
        public AbpCommonHub(IOnlineClientManager onlineClientManager, IOnlineClientInfoProvider clientInfoProvider)
            : base(onlineClientManager, clientInfoProvider)
        {
        }

        public void Register()
        {
            Logger.LogDebug("A client is registered: " + Context.ConnectionId);
        }
    }
}
