using System;

namespace Dignite.Abp.Identity
{
    public class GetOrganizationUnitsInput
    {
        public Guid? ParentId { get; set; }
        public bool Recursive { get; set; }
    }
}
