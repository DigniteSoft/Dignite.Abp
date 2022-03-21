using System;

namespace Dignite.Abp.Identity
{
    public class OrganizationUnitMoveInput
    {

        public Guid? TargetParentId { get; set; }

        public Guid? TargetBeforeId  { get;set; }
    }
}
