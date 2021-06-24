using System;

namespace Dignite.Abp.Notifications
{
    /// <summary>
    /// Arguments for <see cref="NotificationDistributionJob"/>.
    /// </summary>
    [Serializable]
    public class NotificationDistributionJobArgs
    {
        /// <summary>
        /// Notification Id.
        /// </summary>
        public Guid NotificationId { get; set; }


        public Guid[] UserIds { get; set; }
        public Guid[] ExcludedUserIds { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationDistributionJobArgs"/> class.
        /// </summary>
        public NotificationDistributionJobArgs(Guid notificationId,
            Guid[] userIds = null,
            Guid[] excludedUserIds = null            
            )
        {
            NotificationId = notificationId;
            UserIds = userIds;
            ExcludedUserIds = excludedUserIds;
        }
    }
}