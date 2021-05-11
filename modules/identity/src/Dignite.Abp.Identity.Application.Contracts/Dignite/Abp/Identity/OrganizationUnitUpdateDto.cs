
using Volo.Abp.Domain.Entities;

namespace Dignite.Abp.Identity
{
    public class OrganizationUnitUpdateDto: OrganizationUnitCreateOrUpdateDtoBase, IHasConcurrencyStamp
    {
        public string ConcurrencyStamp { get; set; }
    }
}
