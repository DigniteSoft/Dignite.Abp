using Dignite.Abp.SignalR.Dignite.Abp.RealTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.SignalR;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.SignalR.Dignite.Abp.SignalR.Hubs
{
    public abstract class OnlineClientHubBase : AbpHub, ITransientDependency
    {
        protected IOnlineClientManager OnlineClientManager { get; }
        protected IOnlineClientInfoProvider OnlineClientInfoProvider { get; }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();

            var client = CreateClientForCurrentConnection();

            Logger.Debug("A client is connected: " + client);

            OnlineClientManager.Add(client);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);

            Logger.Debug("A client is disconnected: " + Context.ConnectionId);

            try
            {
                OnlineClientManager.Remove(Context.ConnectionId);
            }
            catch (Exception ex)
            {
                Logger.Warn(ex.ToString(), ex);
            }
        }

        protected virtual IOnlineClient CreateClientForCurrentConnection()
        {
            return OnlineClientInfoProvider.CreateClientForCurrentConnection(Context);
        }

    }
}
