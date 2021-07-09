using Dignite.Abp.RealTime;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.SignalR;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.SignalR.Dignite.Abp.SignalR.Hubs
{
    public abstract class OnlineClientHubBase : AbpHub, ITransientDependency
    {
        protected IOnlineClientManager OnlineClientManager { get; }
        protected IOnlineClientInfoProvider OnlineClientInfoProvider { get; }

        protected OnlineClientHubBase(
            IOnlineClientManager onlineClientManager,
            IOnlineClientInfoProvider clientInfoProvider)
        {
            OnlineClientManager = onlineClientManager;
            OnlineClientInfoProvider = clientInfoProvider;
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();

            var client = CreateClientForCurrentConnection();

            Logger.LogDebug("A client is connected: " + client);

            OnlineClientManager.Add(client);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);

            Logger.LogDebug("A client is disconnected: " + Context.ConnectionId);

            try
            {
                OnlineClientManager.Remove(Context.ConnectionId);
            }
            catch (Exception ex)
            {
                Logger.LogWarning(ex.ToString(), ex);
            }
        }

        protected virtual IOnlineClient CreateClientForCurrentConnection()
        {
            return OnlineClientInfoProvider.CreateClientForCurrentConnection(Context);
        }

    }
}
