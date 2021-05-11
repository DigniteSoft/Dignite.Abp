
using Volo.Abp.Application.Dtos;

namespace Dignite.Abp.Identity
{
    public class GetOrganizationUnitMembersInput: PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
