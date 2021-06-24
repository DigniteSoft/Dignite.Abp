using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Microsoft.Extensions.Logging;
using Volo.Abp.Settings;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.MultiTenancy;
using Microsoft.Extensions.Logging.Abstractions;
using System.Security.Claims;
using Volo.Abp.Security.Claims;

namespace Dignite.Abp.Notifications
{
    /// <summary>
    /// Used to distribute notifications to users.
    /// </summary>
    public class DefaultNotificationDistributer : INotificationDistributer, ITransientDependency
    {
        protected NotificationOptions Options { get; }
        protected INotificationDefinitionManager NotificationDefinitionManager { get; }
        protected INotificationStore NotificationStore { get; }
        protected ILogger Logger { get; }
        protected ISettingProvider SettingProvider { get; }
        protected IServiceProvider ServiceProvider { get; }
        protected ICurrentTenant CurrentTenant { get; }
        protected ICurrentPrincipalAccessor CurrentPrincipalAccessor { get; }

        public DefaultNotificationDistributer(
            NotificationOptions notificationOptions, 
            INotificationDefinitionManager notificationDefinitionManager, 
            INotificationStore notificationStore,
            ILoggerFactory loggerFactory, 
            ISettingProvider settingProvider, 
            IServiceProvider serviceProvider, 
            ICurrentTenant currentTenant,
            ICurrentPrincipalAccessor currentPrincipalAccessor)
        {
            Options = notificationOptions;
            NotificationDefinitionManager = notificationDefinitionManager;
            NotificationStore = notificationStore;
            Logger = loggerFactory?.CreateLogger(GetType().FullName) ?? NullLogger.Instance;
            SettingProvider = settingProvider;
            ServiceProvider = serviceProvider;
            CurrentTenant = currentTenant;
            CurrentPrincipalAccessor = currentPrincipalAccessor;
        }

        public async Task DistributeAsync(Guid notificationId,
            Guid[] userIds = null,
            Guid[] excludedUserIds = null)
        {
            var notificationInfo = await NotificationStore.GetNotificationOrNullAsync(notificationId);
            if (notificationInfo == null)
            {
                Logger.LogWarning("NotificationDistributionJob can not continue since could not found notification by id: " + notificationId);
                return;
            }

            var users = await GetUsersAsync(notificationInfo,userIds,excludedUserIds);

            var userNotifications = await SaveUserNotificationsAsync(users, notificationInfo);

            await NotifyAsync(userNotifications.ToArray());
        }

        protected virtual async Task<Guid[]> GetUsersAsync(NotificationInfo notificationInfo,
            Guid[] userIds = null,
            Guid[] excludedUserIds = null)
        {
            List<Guid> targetUserIds;

            if (!userIds.IsNullOrEmpty())
            {
                //Directly get from UserIds
                targetUserIds = new List<Guid>(userIds);
            }
            else
            {
                using (CurrentTenant.Change(notificationInfo.TenantId))
                {
                    //Get subscribed users
                    List<NotificationSubscriptionInfo> subscriptions = await NotificationStore.GetSubscriptionsAsync(
                                notificationInfo.NotificationName,
                                notificationInfo.EntityTypeName,
                                notificationInfo.EntityId
                                );

                    //Remove invalid subscriptions
                    foreach (var subscription in subscriptions)
                    {
                        var claimsPrincipal = new ClaimsPrincipal(
                            new ClaimsIdentity(new Claim[] {
                                new Claim(AbpClaimTypes.UserId,subscription.UserId.ToString()),
                                new Claim(AbpClaimTypes.Role,"½ÇÉ«1"),
                                new Claim(AbpClaimTypes.Role,"½ÇÉ«2"),
                                new Claim(AbpClaimTypes.Role,"½ÇÉ«3"),
                                new Claim(AbpClaimTypes.TenantId,CurrentTenant.Id?.ToString())
                            }));

                        using (CurrentPrincipalAccessor.Change(claimsPrincipal))
                        {
                            if (
                                !await SettingProvider.GetAsync<bool>(NotificationSettingNames.ReceiveNotifications) ||
                                !await NotificationDefinitionManager.IsAvailableAsync(notificationInfo.NotificationName)
                               )
                            {
                                subscriptions.RemoveAll(s => s.UserId == subscription.UserId);
                            }
                        }
                    }

                    //Get user ids
                    targetUserIds = subscriptions
                        .Select(s => s.UserId)
                        .ToList();
                }
            }

            if (!excludedUserIds.IsNullOrEmpty())
            {
                //Exclude specified users.
                targetUserIds.RemoveAll(uid => excludedUserIds.Any(euid => euid.Equals(uid)));
            }

            return targetUserIds.ToArray();
        }



        protected virtual async Task<List<UserNotificationInfo>> SaveUserNotificationsAsync(Guid[] users, NotificationInfo notificationInfo)
        {
            var userNotifications = new List<UserNotificationInfo>();
            foreach (var user in users)
            {
                var userNotification = new UserNotificationInfo(user, notificationInfo.Id, notificationInfo.TenantId);
                await NotificationStore.InsertUserNotificationAsync(userNotification);
                userNotifications.Add(userNotification);
            }
            return userNotifications;
        }



        protected virtual async Task NotifyAsync(UserNotificationInfo[] userNotifications)
        {
            foreach (var notifierType in Options.Notifiers)
            {
                try
                {
                    var notifier = ServiceProvider.GetRequiredService(notifierType) as IRealTimeNotifier;
                    await notifier.SendNotificationsAsync(userNotifications);
                }
                catch (Exception ex)
                {
                    Logger.LogWarning(ex.ToString(), ex);
                }
            }
        }

    }
}
