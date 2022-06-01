

using System;

namespace Dignite.Abp.Identity
{
    public class OrganizationUnitCreateDto: OrganizationUnitCreateOrUpdateDtoBase
    {
        public OrganizationUnitCreateDto():base()
        {
        }

        public virtual Guid? ParentId { get; set; }
    }
}
