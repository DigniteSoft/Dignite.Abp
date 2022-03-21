using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Volo.Abp.Data;

namespace Dignite.Abp.Identity
{
    public class IdentityRoleDto : Volo.Abp.Identity.IdentityRoleDto
    {
        public IdentityRoleDto()
        {
        }

        public IdentityRoleDto(Volo.Abp.Identity.IdentityRoleDto dto)
        {
            Name = dto.Name;
            IsDefault = dto.IsDefault;
            IsStatic = dto.IsStatic;
            IsPublic = dto.IsPublic;
            ConcurrencyStamp= dto.ConcurrencyStamp;
            ExtraProperties = dto.ExtraProperties;
        }

        /// <summary>
        /// 上级角色ID
        /// </summary>
        public Guid? ParentId
        {
            get
            {
                return this.GetProperty<Guid?>(IdentityRoleExtraPropertyNames.ParentIdName, null);
            }
            set
            {
                this.SetProperty(IdentityRoleExtraPropertyNames.ParentIdName, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public IList<IdentityRoleDto> Children { get; set; }
    }
}