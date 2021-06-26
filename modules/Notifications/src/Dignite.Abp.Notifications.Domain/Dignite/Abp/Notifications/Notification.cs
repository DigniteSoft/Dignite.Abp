using System;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace Dignite.Abp.Notifications
{
    public class Notification : BasicAggregateRoot<Guid>, IHasCreationTime, IMultiTenant
    {
        public Notification()
        {
        }

        public Notification(NotificationInfo notification)
            :this(notification.NotificationName,
                 notification.Data,
                 notification.EntityTypeName,
                 notification.EntityId,
                 notification.Severity,
                 notification.CreationTime,
                 notification.TenantId)
        {
        }

        public Notification(string notificationName, NotificationData data, string entityTypeName, string entityId, NotificationSeverity severity, DateTime creationTime, Guid? tenantId)
        {
            NotificationName = notificationName;
            Data = data;
            EntityTypeName = entityTypeName;
            EntityId = entityId;
            Severity = severity;
            CreationTime = creationTime;
            TenantId = tenantId;
        }

        /// <summary>
        /// Unique notification name.
        /// </summary>
        public string NotificationName { get; set; }


        /// <summary>
        /// Can be used to add custom properties to this notification.
        /// </summary>
        public NotificationData Data { get; set; }

        /// <summary>
        /// Gets/sets entity type name, if this is an entity level notification.
        /// It's FullName of the entity type.
        /// </summary>
        public string EntityTypeName { get; set; }


        /// <summary>
        /// Gets/sets primary key of the entity, if this is an entity level notification.
        /// </summary>
        public string EntityId { get; set; }

        /// <summary>
        /// Notification severity.
        /// </summary>
        public NotificationSeverity Severity { get; set; }

        /// <summary>
        /// Creation time
        /// </summary>
        public DateTime CreationTime { get; set; }

        public Guid? TenantId { get; set; }
    }
}
