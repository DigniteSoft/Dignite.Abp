using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Data;

namespace Dignite.Abp.Identity
{
    public class IdentityRoleDto : ExtensibleEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Name { get; set; }

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


        public bool IsDefault { get; set; }
        
        public bool IsStatic { get; set; }

        public bool IsPublic { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}