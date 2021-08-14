using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
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
        private const string AllOrganizationUnitsListCacheName = "GetAllOrganizationUnitsList";


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

        //[Authorize(OrganizationUnitPermissions.OrganizationUnitLookup.Default)]
        public virtual async Task<OrganizationUnitDto> GetAsync(Guid id)
        {
            var dto = ObjectMapper.Map<OrganizationUnit, OrganizationUnitDto>(
                await OrganizationUnitRepository.GetAsync(id,false)
                );
            dto.ChildrenCount = (await OrganizationUnitRepository.GetChildrenAsync(dto.Id, false)).Count;

            return dto;
        }

        //[Authorize(OrganizationUnitPermissions.OrganizationUnitLookup.Default)]
        public virtual async Task<ListResultDto<OrganizationUnitDto>> GetChildrenAsync(Guid? parentId, bool recursive = false)
        {
            List<OrganizationUnit> organizationUnits;
            if (recursive)
            {
                if (!parentId.HasValue)
                {
                    organizationUnits = await GetAllListAsync();
                }
                else
                {
                    var code = await OrganizationUnitManager.GetCodeOrDefaultAsync(parentId.Value);
                    organizationUnits = await OrganizationUnitRepository.GetAllChildrenWithParentCodeAsync(code, parentId, includeDetails: false);
                }
            }
            else
            {
                organizationUnits = await OrganizationUnitRepository.GetChildrenAsync(parentId, includeDetails: false);
            }

            var dtoList = ObjectMapper.Map<List<OrganizationUnit>, List<OrganizationUnitDto>>(organizationUnits);

            if (recursive)
            {
                foreach (var organizationUnit in dtoList)
                {
                    organizationUnit.ChildrenCount = dtoList.Count(ou => ou.ParentId == organizationUnit.Id);
                }
            }
            else
            {
                foreach (var organizationUnit in dtoList)
                {
                    organizationUnit.ChildrenCount = (await OrganizationUnitRepository.GetChildrenAsync(organizationUnit.Id, false)).Count;
                }
            }

            return new ListResultDto<OrganizationUnitDto>(
                dtoList.OrderBy(ou => ou.Position)
                .ThenBy(ou=>ou.Code)
                .ToList()
                );
        }


        //[Authorize(OrganizationUnitPermissions.OrganizationUnitLookup.Default)]
        public virtual async Task<ListResultDto<OrganizationUnitDto>> SearchAsync(string filter)
        {
            var allOrganizationUnits = await GetAllListAsync();
            var result = allOrganizationUnits.Where(ou => ou.DisplayName.Contains(filter));
            List<OrganizationUnit> resultAndParents = new List<OrganizationUnit>();
            foreach (var organizationUnit in result)
            {
                resultAndParents = resultAndParents.Union(allOrganizationUnits.Where(ou => organizationUnit.Code.StartsWith(ou.Code))).ToList();
            }

            List<OrganizationUnit> list = new List<OrganizationUnit>();
            foreach (var organizationUnit in resultAndParents)
            {
                if (resultAndParents.Any(ou => ou.ParentId == organizationUnit.Id))
                {
                    list.AddRange(allOrganizationUnits.Where(ou => ou.ParentId == organizationUnit.Id));
                }
            }

            list = list.Union(resultAndParents).ToList();

            var dtoList = ObjectMapper.Map<List<OrganizationUnit>, List<OrganizationUnitDto>>(list);
            foreach (var organizationUnit in dtoList)
            {
                organizationUnit.ChildrenCount = allOrganizationUnits.Count(ou => ou.ParentId == organizationUnit.Id);
            }

            return new ListResultDto<OrganizationUnitDto>(
                dtoList.OrderBy(ou => ou.Position)
                .ThenBy(ou => ou.Code)
                .ToList()
                );
        }

        //[Authorize(OrganizationUnitPermissions.OrganizationUnitLookup.Default)]
        public virtual async Task<ListResultDto<OrganizationUnitDto>> GetParentsAsync(Guid id)
        {
            var allOrganizationUnits = await GetAllListAsync();
            var organizationUnit = allOrganizationUnits.First(ou => ou.Id == id);
            var list = allOrganizationUnits.Where(ou => organizationUnit.Code.StartsWith(ou.Code) && ou.Id != id).ToList();

            var dtoList = ObjectMapper.Map<List<OrganizationUnit>, List<OrganizationUnitDto>>(list);
            foreach (var dto in dtoList)
            {
                dto.ChildrenCount = allOrganizationUnits.Count(ou => ou.ParentId == dto.Id);
            }

            return new ListResultDto<OrganizationUnitDto>(
                dtoList.OrderBy(ou => ou.Code)
                .ToList()
                );
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
                list = list.Where(ou => !list.Any(y => ou.Code.StartsWith(y.Code) && y.Id!=ou.Id))
                    .ToList();
            }


            var dtoList = ObjectMapper.Map<List<OrganizationUnit>, List<OrganizationUnitDto>>(list);
            foreach (var dto in dtoList)
            {
                dto.ChildrenCount = (await OrganizationUnitRepository.GetChildrenAsync(dto.Id, false)).Count;
            }

            return new ListResultDto<OrganizationUnitDto>(
                dtoList.OrderBy(ou => ou.Code)
                .ToList()
                );
        }

        [Authorize(OrganizationUnitPermissions.OrganizationUnits.Create)]
        public virtual async Task<GetOrganizationUnitForEditOutput> NewAsync(Guid? parentId)
        {
            var output = new GetOrganizationUnitForEditOutput();
            output.OrganizationUnit = new OrganizationUnitEditDto();
            output.RoleIds = new Guid[0];
            output.AvailableRoles = await GetAvailableRolesAsync(parentId);

            return output;
        }

        [Authorize(OrganizationUnitPermissions.OrganizationUnits.Update)]
        public virtual async Task<GetOrganizationUnitForEditOutput> GetOrganizationUnitForEditAsync(Guid id)
        {
            var ou = await OrganizationUnitRepository.GetAsync(id, true);
            var output = new GetOrganizationUnitForEditOutput();
            output.OrganizationUnit = ObjectMapper.Map<OrganizationUnit, OrganizationUnitEditDto>(ou);
            output.RoleIds = ou.Roles.Select(r=>r.RoleId).ToArray();
            output.AvailableRoles = await GetAvailableRolesAsync(ou.ParentId);

            return output;
        }

        [Authorize]
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
            await RepairPosition(children);//

            var ou = new OrganizationUnit(
                GuidGenerator.Create(),
                input.DisplayName,
                input.ParentId,
                CurrentTenant.Id);
            ou.SetProperty(OrganizationUnitExtraPropertyNames.IsActiveName, input.IsActive);
            ou.SetProperty(OrganizationUnitExtraPropertyNames.PositionName, 
                children.Select(c => c.GetProperty<int>(OrganizationUnitExtraPropertyNames.PositionName))
                        .DefaultIfEmpty(0).Max()+1);

            foreach (var roleId in input.RoleIds)
            {
                ou.AddRole(roleId);
            }

            await OrganizationUnitManager.CreateAsync(ou);

            //remove cache
            await CacheOrganizationUnits.RemoveAsync(AllOrganizationUnitsListCacheName);

            return ObjectMapper.Map<OrganizationUnit, OrganizationUnitDto>(ou);
        }

        [Authorize]
        public virtual async Task<OrganizationUnitDto> UpdateAsync(Guid id, OrganizationUnitUpdateDto input)
        {
            var ou = await OrganizationUnitRepository.GetAsync(id, true);
            await AuthorizationService.CheckAsync(ou, CommonOperations.Update);

            ou.DisplayName = input.DisplayName;
            ou.ConcurrencyStamp = input.ConcurrencyStamp;
            ou.SetProperty(OrganizationUnitExtraPropertyNames.IsActiveName, input.IsActive);

            foreach (var roleId in ou.Roles.Select(our => our.RoleId).Except(input.RoleIds))
            {
                ou.RemoveRole(roleId);
            }
            foreach (var roleId in input.RoleIds.Except(ou.Roles.Select(our => our.RoleId)))
            {
                ou.AddRole(roleId);
            }

            await OrganizationUnitManager.UpdateAsync(ou);

            //remove cache
            await CacheOrganizationUnits.RemoveAsync(AllOrganizationUnitsListCacheName);

            return ObjectMapper.Map<OrganizationUnit, OrganizationUnitDto>(ou);
        }

        [Authorize]
        public virtual async Task DeleteAsync(Guid id)
        {
            var ou = await OrganizationUnitRepository.GetAsync(id, false);
            await AuthorizationService.CheckAsync(ou, CommonOperations.Delete);
            await OrganizationUnitManager.DeleteAsync(ou.Id);
        }

        [Authorize]
        public virtual async Task MoveAsync(Guid id, OrganizationUnitMoveInput input)
        {
            var ou = await OrganizationUnitRepository.GetAsync(id, false);
            await AuthorizationService.CheckAsync(ou, CommonOperations.Update);
            if (input.ParentId != ou.ParentId)
            {
                if (input.ParentId.HasValue)
                {
                    var parentOrganizationUnit = await OrganizationUnitRepository.GetAsync(input.ParentId.Value, false);
                    await AuthorizationService.CheckAsync(parentOrganizationUnit, CommonOperations.Create);
                }
                else
                {
                    await AuthorizationService.CheckAsync(null, CommonOperations.Create);
                }
            }

            await OrganizationUnitManager.MoveAsync(ou.Id, input.ParentId);

            var children = await OrganizationUnitRepository.GetChildrenAsync(input.ParentId, false);
            await UpdatePositionAsync(ou, children, input.BeforeOrganizationUnitId);
        }

        [Authorize(OrganizationUnitPermissions.OrganizationUnitLookup.Default)]
        public virtual async Task<ListResultDto<IdentityRoleDto>> GetRolesAsync(Guid id)
        {
            var ou = await OrganizationUnitRepository.GetAsync(id, false);

            return new ListResultDto<IdentityRoleDto>(
                ObjectMapper.Map<List<IdentityRole>, List<IdentityRoleDto>>(
                    await OrganizationUnitRepository.GetRolesAsync(ou)
                ));
        }

        [Authorize(OrganizationUnitPermissions.OrganizationUnitLookup.Default)]
        public virtual async Task<PagedResultDto<IdentityUserDto>> GetMembersAsync(Guid id, GetOrganizationUnitMembersInput input)
        {
            var ou = await OrganizationUnitRepository.GetAsync(id, false);
            var count = await OrganizationUnitRepository.GetMembersCountAsync(ou, input.Filter);
            var members = await OrganizationUnitRepository.GetMembersAsync(ou, input.Sorting,input.MaxResultCount,input.SkipCount,input.Filter);

            return new PagedResultDto<IdentityUserDto>(count,
                ObjectMapper.Map<List<IdentityUser>, List<IdentityUserDto>>(members)
                );
        }

        [Authorize]
        public virtual async Task AddMembersAsync(Guid id, OrganizationUnitAddMembersInput input)
        {
            var ou = await OrganizationUnitRepository.GetAsync(id, false);
            await AuthorizationService.CheckAsync(ou, CommonOperations.Update);
            foreach (var userId in input.UserIds)
            {
                var user = await UserManager.GetByIdAsync(userId);
                await UserManager.AddToOrganizationUnitAsync(user, ou);
            }
        }

        [Authorize]
        public virtual async Task RemoveMembersAsync(Guid id, OrganizationUnitRemoveMembersInput input)
        {
            var ou = await OrganizationUnitRepository.GetAsync(id, false);
            await AuthorizationService.CheckAsync(ou, CommonOperations.Update);
            var availableRoles = (await GetAvailableRolesAsync(ou.ParentId)).Select(r=>r.Name).ToArray();
            foreach (var userId in input.UserIds)
            {
                var user = await UserManager.GetByIdAsync(userId);
                var userRoleNames = (await RoleRepository.GetListAsync())
                    .Where(r => user.Roles.Select(ur => ur.RoleId).Contains(r.Id))
                    .Select(r => r.Name).ToArray();


                await UserManager.SetRolesAsync(user, userRoleNames.Except(availableRoles));
                await UserManager.RemoveFromOrganizationUnitAsync(user, ou);
            }
        }

        [Authorize(OrganizationUnitPermissions.OrganizationUnitLookup.Default)]
        public virtual async Task<ListResultDto<IdentityRoleDto>> GetMemberAssignableRolesAsync(Guid id, Guid userId)
        {
            var ou = await OrganizationUnitRepository.GetAsync(id, false);
            var availableRoles = await GetAvailableRolesAsync(ou.ParentId);
            var userRoles = ObjectMapper.Map<List<IdentityRole>, List<IdentityRoleDto>>(
                 await UserRepository.GetRolesAsync(userId, false)
                );

            var assignableRoles = new List<IdentityRoleDto>();
            foreach (var ur in userRoles)
            {
                ur.IsStatic = true;
                assignableRoles.Add(ur);
            }
            foreach (var avr in availableRoles)
            {
                if (!assignableRoles.Any(asr => asr.Id == avr.Id))
                {
                    assignableRoles.Add(avr);
                }
            }

            return new ListResultDto<IdentityRoleDto>( assignableRoles);
        }

        [Authorize]
        public virtual async Task UpdateMemberRolesAsync(Guid id, Guid userId, OrganizationUnitUpdateMemberRolesInput input)
        {
            var ou = await OrganizationUnitRepository.GetAsync(id, false);
            await AuthorizationService.CheckAsync(ou, CommonOperations.Update);
            var availableRoles = (await GetAvailableRolesAsync(ou.ParentId)).Select(r => r.Name).ToArray();
            input.RoleNames = input.RoleNames.Intersect(availableRoles).ToArray();
            var user = await UserManager.GetByIdAsync(userId);
            var userRoleNames = (await RoleRepository.GetListAsync())
                .Where(r => user.Roles.Select(ur => ur.RoleId).Contains(r.Id))
                .Select(r => r.Name).ToArray();

            await UserManager.SetRolesAsync(user, userRoleNames.Except(availableRoles).Union(input.RoleNames));
        }

        public virtual async Task<OrganizationUnitDto> FindByCodeAsync(string code)
        {
            var allOrganizationUnits = await OrganizationUnitRepository.GetAllChildrenWithParentCodeAsync(code, null, false);
            var organizationUnit = allOrganizationUnits.FirstOrDefault(ou=> ou.Code == code);
            if (organizationUnit == null)
            {
                return null;
            }
            else
            {
                var dto = ObjectMapper.Map<OrganizationUnit, OrganizationUnitDto>(organizationUnit);
                dto.ChildrenCount = (await OrganizationUnitRepository.GetChildrenAsync(dto.Id, false)).Count;

                return dto;
            }
        }

        protected async Task<List<OrganizationUnit>> GetAllListAsync()
        {
            return await OrganizationUnitRepository.GetListAsync(includeDetails: false);
            //return await CacheOrganizationUnits.GetOrAddAsync(
            //     AllOrganizationUnitsListCacheName, //Cache key
            //    async () => await OrganizationUnitRepository.GetListAsync(includeDetails: false),
            //    () => new DistributedCacheEntryOptions
            //    {
            //        AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(10)
            //    }
            //);
        }

        protected async Task<IReadOnlyList<IdentityRoleDto>> GetAvailableRolesAsync(Guid? parentOrganizationUnitId=null)
        {
            var allRoles = (await RoleRepository.GetListAsync())
                .Where(r => r.IsPublic 
                    && (!r.IsDefault || !r.IsStatic)
                    ).ToList();

            if (!parentOrganizationUnitId.HasValue)
            {
                return ObjectMapper.Map<List<IdentityRole>,List<IdentityRoleDto>>( allRoles.ToList());
            }
            else
            {
                var ouRoles = await GetOrganizationUnitRoles(parentOrganizationUnitId.Value);
                if (ouRoles.Any())
                {
                    List<IdentityRole> roles = new List<IdentityRole>(ouRoles);
                    GetChildRoles(
                        allRoles.Where(r => r.GetProperty<Guid?>(IdentityRoleExtraPropertyNames.ParentIdName, null).HasValue),
                        ouRoles,
                        roles);

                    return ObjectMapper.Map<List<IdentityRole>, List<IdentityRoleDto>>(roles);
                }
                else
                {
                    return new List<IdentityRoleDto>();
                }
            }
        }

        private async Task<List<IdentityRole>> GetOrganizationUnitRoles(Guid organizationUnitId)
        {
            var ou = await OrganizationUnitRepository.GetAsync(organizationUnitId, false);
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

        private async Task UpdatePositionAsync(OrganizationUnit organizationUnit, List<OrganizationUnit> children, Guid? beforeOrganizationUnitId)
        {
            await RepairPosition(children);

            int newPosition = 1;
            if (beforeOrganizationUnitId.HasValue)
            {
                var beforeOrganizationUnit = children.Single(ou => ou.Id == beforeOrganizationUnitId.Value);
                newPosition = beforeOrganizationUnit.GetProperty<int>(OrganizationUnitExtraPropertyNames.PositionName) + 1;
            }

            foreach (var ou in children)
            {
                if (ou.GetProperty<int>(OrganizationUnitExtraPropertyNames.PositionName) >= newPosition
                    && ou.Id != organizationUnit.Id
                    )
                {
                    ou.SetProperty(OrganizationUnitExtraPropertyNames.PositionName,
                        ou.GetProperty<int>(OrganizationUnitExtraPropertyNames.PositionName) + 1);
                    await OrganizationUnitManager.UpdateAsync(ou);
                }
            }

            organizationUnit.SetProperty(OrganizationUnitExtraPropertyNames.PositionName, newPosition);
            await OrganizationUnitManager.UpdateAsync(organizationUnit);
        }

        private async Task RepairPosition(List<OrganizationUnit> children)
        {
            foreach (var ou in children)
            {
                if (!ou.HasProperty(OrganizationUnitExtraPropertyNames.PositionName))
                {
                    var maxPosition = children.Where(c => c.HasProperty(OrganizationUnitExtraPropertyNames.PositionName))
                        .Select(c => c.GetProperty<int>(OrganizationUnitExtraPropertyNames.PositionName))
                        .DefaultIfEmpty(0).Max();
                    ou.SetProperty(OrganizationUnitExtraPropertyNames.PositionName, maxPosition + 1);
                    await OrganizationUnitManager.UpdateAsync(ou);
                }
            }
        }
    }
}
