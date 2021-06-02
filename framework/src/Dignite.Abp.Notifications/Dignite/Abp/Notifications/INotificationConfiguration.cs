using Volo.Abp.Collections;
using System;

namespace Dignite.Abp.Notifications
{
    /// <summary>
    /// Used to configure notification system.
    /// </summary>
    public interface INotificationConfiguration
    {
        /// <summary>
        /// Notification providers.
        /// </summary>
        ITypeList<NotificationProvider> Providers { get; }

        /// <summary>
        /// A list of contributors for notification notifying process.
        /// </summary>
        ITypeList<IRealTimeNotifier> Notifiers { get; }
    }
}
