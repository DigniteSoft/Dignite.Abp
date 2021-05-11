using System;
using System.ComponentModel.DataAnnotations;

namespace Dignite.Abp.Identity
{
    public class OrganizationUnitRemoveMembersInput
    {
        [Required]
        public Guid[] UserIds { get; set; }
    }
}
