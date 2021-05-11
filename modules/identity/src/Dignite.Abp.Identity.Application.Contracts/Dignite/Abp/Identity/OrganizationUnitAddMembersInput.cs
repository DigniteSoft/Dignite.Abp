using System;
using System.ComponentModel.DataAnnotations;

namespace Dignite.Abp.Identity
{
    public class OrganizationUnitAddMembersInput
    {
        [Required]
        public Guid[] UserIds { get; set; }
    }
}
