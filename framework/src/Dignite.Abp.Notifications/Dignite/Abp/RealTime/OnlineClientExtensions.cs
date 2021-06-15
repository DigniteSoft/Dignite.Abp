using JetBrains.Annotations;

namespace Dignite.Abp.Notifications.RealTime
{
    public static class OnlineClientExtensions
    {
        [CanBeNull]
        public static UserIdentifier ToUserIdentifierOrNull(this IOnlineClient onlineClient)
        {
            return onlineClient.UserId.HasValue
                ? new UserIdentifier(onlineClient.TenantId, onlineClient.UserId.Value)
                : null;
        }
    }
}
