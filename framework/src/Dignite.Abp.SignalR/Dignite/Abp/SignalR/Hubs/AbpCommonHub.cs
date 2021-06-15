using Dignite.Abp.Notifications.RealTime;
using Dignite.Abp.SignalR.Dignite.Abp.RealTime;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
