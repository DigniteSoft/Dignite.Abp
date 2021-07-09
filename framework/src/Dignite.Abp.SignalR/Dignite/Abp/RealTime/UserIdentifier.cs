using System;

namespace Dignite.Abp.RealTime
{
    /// <summary>
    /// Used to identify a user.
    /// </summary>
    [Serializable]
    public class UserIdentifier : IUserIdentifier
    {
        /// <summary>
        /// Tenant Id of the user.
        /// Can be null for host users in a multi tenant application.
        /// </summary>
        public Guid? TenantId { get; protected set; }

        /// <summary>
        /// Id of the user.
        /// </summary>
        public Guid UserId { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserIdentifier"/> class.
        /// </summary>
        protected UserIdentifier()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserIdentifier"/> class.
        /// </summary>
        /// <param name="tenantId">Tenant Id of the user.</param>
        /// <param name="userId">Id of the user.</param>
        public UserIdentifier(Guid? tenantId, Guid userId)
        {
            TenantId = tenantId;
            UserId = userId;
        }

    }
}
