using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Identity;
using Volo.Abp.Validation;

namespace Dignite.Abp.Identity
{
    public class OrganizationUnitCreateOrUpdateDtoBase
    {
        public OrganizationUnitCreateOrUpdateDtoBase()
        {
            IsActive = true;
            RoleIds = new List<Guid>();
        }


        /// <summary>
        /// Display name of this OrganizationUnit.
        /// </summary>
        [Required]
        [DynamicStringLength(typeof(OrganizationUnitConsts), nameof(OrganizationUnitConsts.MaxDisplayNameLength))]
        public virtual string DisplayName { get; set; }

        /// <summary>
        /// 是否激活状态
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 组织单元的角色
        /// </summary>
        /// <remarks>
        /// 组织单元下的用户自动拥有组织单元的角色
        /// </remarks>
        public List<Guid> RoleIds { get; set; }
    }
}
