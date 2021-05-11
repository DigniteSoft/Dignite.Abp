using System;
using System.Text.Json.Serialization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Data;

namespace Dignite.Abp.Identity
{
    public class OrganizationUnitDto:ExtensibleEntityDto<Guid>
    {
        /// <summary>
        /// Parent <see cref="OrganizationUnitDto"/> Id.
        /// Null, if this OU is a root.
        /// </summary>
        public virtual Guid? ParentId { get;  set; }

        /// <summary>
        /// Hierarchical Code of this organization unit.
        /// Example: "00001.00042.00005".
        /// This is a unique code for an OrganizationUnit.
        /// It's changeable if OU hierarchy is changed.
        /// </summary>
        public virtual string Code { get;  set; }

        /// <summary>
        /// Display name of this OrganizationUnit.
        /// </summary>
        public virtual string DisplayName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ChildrenCount { get; set; }

        /// <summary>
        /// 是否激活状态
        /// </summary>
        public bool IsActive
        {
            get
            {
                return this.GetProperty<bool>(OrganizationUnitExtraPropertyNames.IsActiveName, true);
            }
            set
            {
                this.SetProperty(OrganizationUnitExtraPropertyNames.IsActiveName, value);
            }
        }

        [JsonIgnore]
        public int Position
        {
            get
            {
                return this.GetProperty<int>(OrganizationUnitExtraPropertyNames.PositionName, 1);
            }
            set
            {
                this.SetProperty(OrganizationUnitExtraPropertyNames.PositionName, value);
            }
        }
    }
}
