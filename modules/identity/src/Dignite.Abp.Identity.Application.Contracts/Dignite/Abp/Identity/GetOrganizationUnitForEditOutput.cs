using System;
using System.Collections.Generic;

namespace Dignite.Abp.Identity
{
    public class GetOrganizationUnitForEditOutput
    {
        public OrganizationUnitEditDto OrganizationUnit { get; set; }

        public Guid[] RoleIds { get; set; }

        public IReadOnlyList<IdentityRoleDto> AvailableRoles { get; set; }
    }
}
