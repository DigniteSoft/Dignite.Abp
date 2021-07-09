using Microsoft.AspNetCore.SignalR;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.RealTime
{
    public interface IOnlineClientInfoProvider : ITransientDependency
    {
        IOnlineClient CreateClientForCurrentConnection(HubCallerContext context);
    }
}
