using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dignite.Abp.SignalR.Dignite.Abp.RealTime
{
    public class OnlineClientInfoProvider : IOnlineClientInfoProvider
    {

        private readonly IClientInfoProvider _clientInfoProvider;

        public OnlineClientInfoProvider(IClientInfoProvider clientInfoProvider)
        {
            _clientInfoProvider = clientInfoProvider;
            Logger = NullLogger.Instance;
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
                Logger.Error("Can not find IP address of the client! connectionId: " + context.ConnectionId);
                Logger.Error(ex.Message, ex);
                return "";
            }
        }
    }
}
