using System;

namespace Dignite.Abp.Identity
{
    public class OrganizationUnitRoleDto
    {
        /// <summary>
        /// Id of the Role.
        /// </summary>
        public virtual Guid RoleId { get;  set; }

        /// <summary>
        /// Id of the <see cref="OrganizationUnitDto"/>.
        /// </summary>
        public virtual Guid OrganizationUnitId { get;  set; }
    }
}
