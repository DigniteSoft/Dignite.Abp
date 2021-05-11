

using System;

namespace Dignite.Abp.Identity
{
    public class OrganizationUnitCreateDto: OrganizationUnitCreateOrUpdateDtoBase
    {
        public virtual Guid? ParentId { get; set; }
    }
}
