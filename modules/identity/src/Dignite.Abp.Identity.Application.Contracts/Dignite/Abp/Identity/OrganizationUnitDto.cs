using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;

namespace Dignite.Abp.Identity
{
    public class OrganizationUnitDto : ExtensibleEntityDto<Guid>, IEquatable<OrganizationUnitDto>, IHasConcurrencyStamp
    {
        public OrganizationUnitDto() : base()
        {
            Children = new List<OrganizationUnitDto>();
        }

        /// <summary>
        /// Parent <see cref="OrganizationUnitDto"/> Id.
        /// Null, if this OU is a root.
        /// </summary>
        public virtual Guid? ParentId { get; set; }

        /// <summary>
        /// Hierarchical Code of this organization unit.
        /// Example: "00001.00042.00005".
        /// This is a unique code for an OrganizationUnit.
        /// It's changeable if OU hierarchy is changed.
        /// </summary>
        public virtual string Code { get; set; }

        /// <summary>
        /// Display name of this OrganizationUnit.
        /// </summary>
        public virtual string DisplayName { get; set; }

        public string ConcurrencyStamp { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonInclude]
        public IList<OrganizationUnitDto> Children { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonInclude]
        public bool HasChild
        {
            get; private set;
        }

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
        public int Sort
        {
            get
            {
                return this.GetProperty<int>(OrganizationUnitExtraPropertyNames.SortName, 1);
            }
            set
            {
                this.SetProperty(OrganizationUnitExtraPropertyNames.SortName, value);
            }
        }

        public bool Equals(OrganizationUnitDto other)
        {
            return this.Id == other.Id;
        }

        public void AddChild(OrganizationUnitDto ou)
        {
            this.HaveChildren(true);
            this.Children.Add(ou);
        }

        public void Remove(OrganizationUnitDto ou)
        {
            this.Children.RemoveAll(c => ou.Id == c.Id);
            if (!Children.Any())
                this.HaveChildren(false);
        }

        public void HaveChildren(bool isHasChild)
        {
            this.HasChild = isHasChild;
        }
    }
}
