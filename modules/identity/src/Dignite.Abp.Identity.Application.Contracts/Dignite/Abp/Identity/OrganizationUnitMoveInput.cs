using System;

namespace Dignite.Abp.Identity
{
    public class OrganizationUnitMoveInput
    {

        public Guid? ParentId { get; set; }

        public Guid? BeforeOrganizationUnitId  { get;set; }
    }
}
