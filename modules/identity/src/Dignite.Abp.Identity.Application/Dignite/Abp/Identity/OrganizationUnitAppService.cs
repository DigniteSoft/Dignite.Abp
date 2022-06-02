using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Caching;
using Volo.Abp.Data;
using Volo.Abp.Identity;

namespace Dignite.Abp.Identity
{
    public class OrganizationUnitAppService : IdentityAppServiceBase, IOrganizationUnitAppService
    {
        protected IIdentityUserRepository UserRepository { get; }
        protected IdentityUserManager UserManager { get; }

        protected IIdentityRoleRepository RoleRepository { get; }
        protected OrganizationUnitManager OrganizationUnitManager { get; }
        protected IOrganizationUnitRepository OrganizationUnitRepository { get; }

        protected IDistributedCache<List<OrganizationUnit>> CacheOrganizationUnits { get; }

        public OrganizationUnitAppService(IIdentityUserRepository userRepository, IdentityUserManager userManager, IIdentityRoleRepository roleRepository, OrganizationUnitManager organizationUnitManager, IOrganizationUnitRepository organizationUnitRepository, IDistributedCache<List<OrganizationUnit>> cacheOrganizationUnits)
        {
            UserRepository = userRepository;
            UserManager = userManager;
            RoleRepository = roleRepository;
            OrganizationUnitManager = organizationUnitManager;
            OrganizationUnitRepository = organizationUnitRepository;
            CacheOrganizationUnits = cacheOrganizationUnits;
        }

        [Authorize(OrganizationUnitPermissions.OrganizationUnits.Default)]
        public virtual async Task<OrganizationUnitDto> GetAsync(Guid id)
        {
            var dto = ObjectMapper.Map<OrganizationUnit, OrganizationUnitDto>(
                await OrganizationUnitRepository.GetAsync(id,false)
                );

            return dto;
        }



        [Authorize(OrganizationUnitPermissions.OrganizationUnits.Default)]
        public virtual async Task<PagedResultDto<OrganizationUnitDto>> GetListAsync(GetOrganizationUnitsInput input)
        {
            List<OrganizationUnit> organizationUnits;
            if (input.Recursive)
            {
                if (!input.ParentId.HasValue)
                {
                    organizationUnits = await GetAllListAsync();
                }
                else
                {
                    var code = await OrganizationUnitManager.GetCodeOrDefaultAsync(input.ParentId.Value);
                    organizationUnits = await OrganizationUnitRepository.GetAllChildrenWithParentCodeAsync(code, input.ParentId, includeDetails: false);
                }
            }
            else
            {
                organizationUnits = await OrganizationUnitRepository.GetChildrenAsync(input.ParentId, includeDetails: false);
            }

            var dto = ObjectMapper.Map<List<OrganizationUnit>, List<OrganizationUnitDto>>(organizationUnits);

            if (input.Recursive)
            {
                var list = new List<OrganizationUnitDto>();
                list.AddRange(dto.Where(p => p.ParentId == input.ParentId).ToList());
                foreach (var ou in list)
                {
                    AddChildren(ou, dto);
                }

                return new PagedResultDto<OrganizationUnitDto>(
                    list.Count,
                    list.OrderBy(ou => ou.Sort)
                    .ThenBy(ou => ou.Code)
                    .ToList()
                    );
            }
            else
            {
                foreach (var ou in dto)
                {
                    ou.HaveChildren(
                        (await OrganizationUnitRepository.GetChildrenAsync(ou.Id, false))
                        .Any()
                        );
                }

                return new PagedResultDto<OrganizationUnitDto>(
                    dto.Count,
                    dto.OrderBy(ou => ou.Sort)
                    .ThenBy(ou => ou.Code)
                    .ToList()
                    );
            }
        }



        [Authorize]
        public virtual async Task<ListResultDto<OrganizationUnitDto>> GetAuthorizedAsync()
        {
            List<OrganizationUnit> list;
            if (await AuthorizationService.IsGrantedAsync(OrganizationUnitPermissions.OrganizationUnits.SuperAuthorization))
            {
                list = await OrganizationUnitRepository.GetChildrenAsync(null, includeDetails: false);
            }
            else
            {
                list = await UserRepository.GetOrganizationUnitsAsync(CurrentUser.Id.Value, false);
                list = list.Where(ou => !list.Any(y => ou.Code.StartsWith(y.Code) && y.Id != ou.Id))
                    .ToList();
            }

            var dto = ObjectMapper.Map<List<OrganizationUnit>, List<OrganizationUnitDto>>(list);
            return new ListResultDto<OrganizationUnitDto>(
                    dto.OrderBy(ou => ou.Sort)
                    .ThenBy(ou => ou.Code)
                    .ToList()
                    );
        }


        [Authorize(OrganizationUnitPermissions.OrganizationUnits.Create)]
        public virtual async Task<OrganizationUnitDto> CreateAsync(OrganizationUnitCreateDto input)
        {
            var parentOrganizationUnit = input.ParentId.HasValue ?
                await OrganizationUnitRepository.GetAsync(input.ParentId.Value, false)
                : null;
            if (parentOrganizationUnit != null)
            {
                await AuthorizationService.CheckAsync(parentOrganizationUnit, CommonOperations.Create);
            }
            else
            {
                await AuthorizationService.CheckAsync(OrganizationUnitPermissions.OrganizationUnits.SuperAuthorization);
            }

            var children = await OrganizationUnitRepository.GetChildrenAsync(input.ParentId, false);
            await RepairSort(children);//

            var ou = new OrganizationUnit(
                GuidGenerator.Create(),
                input.DisplayName,
                input.ParentId,
                CurrentTenant.Id);
            ou.SetProperty(OrganizationUnitExtraPropertyNames.IsActiveName, input.IsActive);
            ou.SetProperty(OrganizationUnitExtraPropertyNames.SortName,
                children.Select(c => c.GetProperty<int>(OrganizationUnitExtraPropertyNames.SortName))
                        .DefaultIfEmpty(0).Max() + 1);

            //todo 这里需要根据父级机构的解难判断添加的角色是否符合逻辑
            //add roles
            foreach (var roleId in input.RoleIds)
            {
                ou.AddRole(roleId);
            }

            await OrganizationUnitManager.CreateAsync(ou);

            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<OrganizationUnit, OrganizationUnitDto>(ou);
        }

        [Authorize(OrganizationUnitPermissions.OrganizationUnits.Update)]
        public virtual async Task<OrganizationUnitDto> UpdateAsync(Guid id, OrganizationUnitUpdateDto input)
        {
            var ou = await OrganizationUnitRepository.GetAsync(id, true);
            await AuthorizationService.CheckAsync(ou, CommonOperations.Update);

            ou.DisplayName = input.DisplayName;
            ou.ConcurrencyStamp = input.ConcurrencyStamp;
            ou.SetProperty(OrganizationUnitExtraPropertyNames.IsActiveName, input.IsActive);


            //todo 这里需要根据父级机构的解难判断添加的角色是否符合逻辑
            foreach (var roleId in ou.Roles.Select(our => our.RoleId).Except(input.RoleIds))
            {
                ou.RemoveRole(roleId);
            }
            foreach (var roleId in input.RoleIds.Except(ou.Roles.Select(our => our.RoleId)))
            {
                ou.AddRole(roleId);
            }

            await OrganizationUnitManager.UpdateAsync(ou);

            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<OrganizationUnit, OrganizationUnitDto>(ou);
        }

        [Authorize(OrganizationUnitPermissions.OrganizationUnits.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            var ou = await OrganizationUnitRepository.GetAsync(id, false);
            await AuthorizationService.CheckAsync(ou, CommonOperations.Delete);
            await OrganizationUnitManager.DeleteAsync(ou.Id);

        }

        [Authorize(OrganizationUnitPermissions.OrganizationUnits.Create)]
        public virtual async Task<OrganizationUnitDto> MoveAsync(Guid id, OrganizationUnitMoveInput input)
        {
            var ou = await OrganizationUnitRepository.GetAsync(id, false);
            if (input.TargetParentId.HasValue)
            {
                var parentOrganizationUnit = await OrganizationUnitRepository.GetAsync(input.TargetParentId.Value, false);
                await AuthorizationService.CheckAsync(parentOrganizationUnit, CommonOperations.Create);
            }
            else
            {
                await AuthorizationService.CheckAsync(OrganizationUnitPermissions.OrganizationUnits.SuperAuthorization);
            }

            //移动
            await OrganizationUnitManager.MoveAsync(ou.Id, input.TargetParentId);

            //更新位置
            var children = await OrganizationUnitRepository.GetChildrenAsync(input.TargetParentId, false);
            await UpdateSortAsync(ou, children, input.TargetBeforeId);

            //
            return await GetAsync(id);
        }

        [Authorize(OrganizationUnitPermissions.OrganizationUnits.Default)]
        public virtual async Task<ListResultDto<IdentityRoleDto>> GetRolesAsync(Guid id)
        {
            var ou = await OrganizationUnitRepository.GetAsync(id, false);
            var dto = ObjectMapper.Map<List<IdentityRole>, List<IdentityRoleDto>>(
                    await OrganizationUnitRepository.GetRolesAsync(ou)
                );

            return new ListResultDto<IdentityRoleDto>(
                dto.BuildIdentityRolesTree()
                );
        }


        [Authorize(OrganizationUnitPermissions.OrganizationUnits.Default)]
        public async Task<ListResultDto<IdentityRoleDto>> GetAvailableRolesAsync(Guid? parentId)
        {
            var allRoles = (await RoleRepository.GetListAsync())
                .Where(r => r.IsPublic
                    && (!r.IsDefault || !r.IsStatic)
                    ).ToList();

            if (!parentId.HasValue)
            {
                return new ListResultDto<IdentityRoleDto>(
                    ObjectMapper.Map<List<IdentityRole>, List<IdentityRoleDto>>(allRoles.ToList())
                    .BuildIdentityRolesTree()
                    );
            }
            else
            {
                var ouRoles = await GetOrganizationUnitRoles(parentId.Value);
                if (ouRoles.Any())
                {
                    List<IdentityRole> roles = new List<IdentityRole>(ouRoles);
                    GetChildRoles(
                        allRoles.Where(r => r.GetProperty<Guid?>(IdentityRoleExtraPropertyNames.ParentIdName, null).HasValue),
                        ouRoles,
                        roles);


                    return new ListResultDto<IdentityRoleDto>(
                        ObjectMapper.Map<List<IdentityRole>, List<IdentityRoleDto>>(roles)
                        .BuildIdentityRolesTree()
                        );
                }
                else
                {
                    return new ListResultDto<IdentityRoleDto>();
                }
            }
        }


        [Authorize(OrganizationUnitPermissions.OrganizationUnits.Default)]
        public virtual async Task<PagedResultDto<IdentityUserDto>> GetMembersAsync(Guid id, GetOrganizationUnitMembersInput input)
        {
            var ou = await OrganizationUnitRepository.GetAsync(id, false);
            var count = await OrganizationUnitRepository.GetMembersCountAsync(ou, input.Filter);
            var members = await OrganizationUnitRepository.GetMembersAsync(ou, input.Sorting, input.MaxResultCount, input.SkipCount, input.Filter);

            return new PagedResultDto<IdentityUserDto>(count,
                ObjectMapper.Map<List<IdentityUser>, List<IdentityUserDto>>(members)
                );
        }

        [Authorize(OrganizationUnitPermissions.OrganizationUnits.MembersManage)]
        public virtual async Task AddMembersAsync(Guid id, Guid[] userIds)
        {
            var ou = await OrganizationUnitRepository.GetAsync(id, false);
            await AuthorizationService.CheckAsync(ou, CommonOperations.Get);
            foreach (var userId in userIds)
            {
                var user = await UserManager.GetByIdAsync(userId);
                await UserManager.AddToOrganizationUnitAsync(user, ou);
            }
        }

        [Authorize(OrganizationUnitPermissions.OrganizationUnits.MembersManage)]
        public virtual async Task RemoveMembersAsync(Guid id, Guid[] userIds)
        {
            var ou = await OrganizationUnitRepository.GetAsync(id, false);
            await AuthorizationService.CheckAsync(ou, CommonOperations.Get);
            foreach (var userId in userIds)
            {
                var user = await UserManager.GetByIdAsync(userId);
                await UserManager.RemoveFromOrganizationUnitAsync(user, ou);
            }
        }



        protected async Task<List<OrganizationUnit>> GetAllListAsync()
        {
            return await OrganizationUnitRepository.GetListAsync(includeDetails: false);
        }


        private async Task<List<IdentityRole>> GetOrganizationUnitRoles(Guid id)
        {
            var ou = await OrganizationUnitRepository.GetAsync(id, false);
            var ouRoles = await OrganizationUnitRepository.GetRolesAsync(ou);

            if (ouRoles.Any())
            {
                return ouRoles;
            }
            else
            {
                if (ou.ParentId.HasValue)
                    return await GetOrganizationUnitRoles(ou.ParentId.Value);
                else
                    return ouRoles;
            }
        }

        private void GetChildRoles(IEnumerable<IdentityRole> allRoles, List<IdentityRole> ouRoles, List<IdentityRole> roles)
        {
            var parentRoles = new List<IdentityRole>();
            foreach (var role in ouRoles)
            {
                foreach (var r in allRoles)
                {
                    if (role.Id == r.GetProperty<Guid>(IdentityRoleExtraPropertyNames.ParentIdName))
                    {
                        roles.Add(r);

                        if (allRoles.Any(ar =>
                            ar.GetProperty<Guid>(IdentityRoleExtraPropertyNames.ParentIdName) == r.Id
                            ))
                        {
                            parentRoles.Add(r);
                        }
                    }
                }
            }

            if (parentRoles.Any())
            {
                GetChildRoles(allRoles, parentRoles, roles);
            }
        }

        private async Task UpdateSortAsync(OrganizationUnit organizationUnit, List<OrganizationUnit> children, Guid? beforeOrganizationUnitId)
        {
            await RepairSort(children);

            int newSort = 1;
            if (beforeOrganizationUnitId.HasValue)
            {
                var beforeOrganizationUnit = children.Single(ou => ou.Id == beforeOrganizationUnitId.Value);
                newSort = beforeOrganizationUnit.GetProperty<int>(OrganizationUnitExtraPropertyNames.SortName) + 1;
            }

            foreach (var ou in children)
            {
                if (ou.GetProperty<int>(OrganizationUnitExtraPropertyNames.SortName) >= newSort
                    && ou.Id != organizationUnit.Id
                    )
                {
                    ou.SetProperty(OrganizationUnitExtraPropertyNames.SortName,
                        ou.GetProperty<int>(OrganizationUnitExtraPropertyNames.SortName) + 1);
                    await OrganizationUnitManager.UpdateAsync(ou);
                }
            }

            organizationUnit.SetProperty(OrganizationUnitExtraPropertyNames.SortName, newSort);
            await OrganizationUnitManager.UpdateAsync(organizationUnit);
        }

        /// <summary>
        /// 修复机构位置数据
        /// </summary>
        /// <param name="children"></param>
        /// <returns></returns>
        private async Task RepairSort(List<OrganizationUnit> children)
        {
            foreach (var ou in children)
            {
                if (!ou.HasProperty(OrganizationUnitExtraPropertyNames.SortName))
                {
                    var maxSort = children.Where(c => c.HasProperty(OrganizationUnitExtraPropertyNames.SortName))
                        .Select(c => c.GetProperty<int>(OrganizationUnitExtraPropertyNames.SortName))
                        .DefaultIfEmpty(0).Max();
                    ou.SetProperty(OrganizationUnitExtraPropertyNames.SortName, maxSort + 1);
                    await OrganizationUnitManager.UpdateAsync(ou);
                }
            }
        }


        protected void AddChildren(OrganizationUnitDto parent, List<OrganizationUnitDto> list)
        {
            var children = list.Where(p => p.ParentId == parent.Id).ToList();
            if (children.Any())
            {
                foreach (var ou in children)
                {
                    parent.AddChild(ou);
                    AddChildren(ou, list);
                }
            }
        }

    }
}
