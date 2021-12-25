using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Settings;
using Volo.Abp.Uow;

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
        protected IUnitOfWorkManager UnitOfWorkManager { get; }


        public DefaultNotificationDistributer(
            IOptions<NotificationOptions> notificationOptions, 
            INotificationDefinitionManager notificationDefinitionManager, 
            INotificationStore notificationStore,
            ILoggerFactory loggerFactory, 
            ISettingProvider settingProvider, 
            IServiceProvider serviceProvider, 
            ICurrentTenant currentTenant,
            IUnitOfWorkManager unitOfWorkManager)
        {
            Options = notificationOptions.Value;
            NotificationDefinitionManager = notificationDefinitionManager;
            NotificationStore = notificationStore;
            Logger = loggerFactory?.CreateLogger(GetType().FullName) ?? NullLogger.Instance;
            SettingProvider = settingProvider;
            ServiceProvider = serviceProvider;
            CurrentTenant = currentTenant;
            UnitOfWorkManager = unitOfWorkManager;
        }

        public async Task DistributeAsync(
            NotificationInfo notification,
            Guid[] userIds = null,
            Guid[] excludedUserIds = null)
        {
            var users = await GetUsersAsync(notification, userIds,excludedUserIds);
            if (users != null && users.Any())
            {
                var userNotifications = await SaveUserNotificationsAsync(users, notification);
                await NotifyAsync(userNotifications.ToArray());
            }
        }

        protected virtual async Task<Guid[]> GetUsersAsync(
            NotificationInfo notificationInfo,
            Guid[] userIds = null,
            Guid[] excludedUserIds = null)
        {
            List<Guid> distributeUserIds;

            if (!userIds.IsNullOrEmpty())
            {
                //Directly get from UserIds
                distributeUserIds = new List<Guid>(userIds);
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
                        if (
                            !await NotificationDefinitionManager.IsAvailableAsync(notificationInfo.NotificationName, subscription.UserId)
                           )
                        {
                            subscriptions.RemoveAll(s => s.UserId == subscription.UserId);
                        }
                    }

                    //Get user ids
                    distributeUserIds = subscriptions
                        .Select(s => s.UserId)
                        .ToList();
                }
            }

            if (!excludedUserIds.IsNullOrEmpty())
            {
                //Exclude specified users.
                distributeUserIds.RemoveAll(uid => excludedUserIds.Any(euid => euid.Equals(uid)));
            }

            return distributeUserIds.ToArray();
        }



        [UnitOfWork]
        protected virtual async Task<List<UserNotificationInfo>> SaveUserNotificationsAsync(Guid[] users, NotificationInfo notificationInfo)
        {
            await NotificationStore.InsertNotificationAsync(notificationInfo);
            await UnitOfWorkManager.Current.SaveChangesAsync();

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
