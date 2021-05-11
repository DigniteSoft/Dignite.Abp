using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Data;

namespace Dignite.Abp.Identity
{
    public class OrganizationUnitEditDto:ExtensibleEntityDto<Guid>
    {

        /// <summary>
        /// Display name of this OrganizationUnit.
        /// </summary>
        public virtual string DisplayName { get; set; }


        /// <summary>
        /// 是否激活状态
        /// </summary>
        public bool IsActive
        {
            get
            {
                return this.GetProperty<bool>(nameof(IsActive), true);
            }
            set
            {
                this.SetProperty(nameof(IsActive), value);
            }
        }
    }
}
