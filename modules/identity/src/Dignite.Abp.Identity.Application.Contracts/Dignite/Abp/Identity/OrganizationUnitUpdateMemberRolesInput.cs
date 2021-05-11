using System.ComponentModel.DataAnnotations;

namespace Dignite.Abp.Identity
{
    public class OrganizationUnitUpdateMemberRolesInput
    {
        [Required]
        public string[] RoleNames { get; set; }
    }
}