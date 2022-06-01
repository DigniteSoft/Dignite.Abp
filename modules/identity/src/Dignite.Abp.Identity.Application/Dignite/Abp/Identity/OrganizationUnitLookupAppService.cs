using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace Dignite.Abp.Identity
{
    public class OrganizationUnitLookupAppService : IdentityAppServiceBase, IOrganizationUnitLookupAppService
    {
        protected OrganizationUnitManager OrganizationUnitManager { get; }
        protected IOrganizationUnitRepository OrganizationUnitRepository { get; }
        public OrganizationUnitLookupAppService(OrganizationUnitManager organizationUnitManager, IOrganizationUnitRepository organizationUnitRepository)
        {
            OrganizationUnitManager = organizationUnitManager;
            OrganizationUnitRepository = organizationUnitRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[Authorize(OrganizationUnitPermissions.OrganizationUnitLookup.Default)]
        public virtual async Task<OrganizationUnitDto> FindByIdAsync(Guid id)
        {
            var dto = ObjectMapper.Map<OrganizationUnit, OrganizationUnitDto>(
                await OrganizationUnitRepository.FindAsync(id, false)
                );

            return dto;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        //[Authorize(OrganizationUnitPermissions.OrganizationUnitLookup.Default)]
        public virtual async Task<OrganizationUnitDto> FindByCodeAsync(string code)
        {
            var list = await OrganizationUnitRepository.GetAllChildrenWithParentCodeAsync(code, null, false);
            return ObjectMapper.Map<OrganizationUnit, OrganizationUnitDto>(
                list.FirstOrDefault(ou => ou.Code == code)
                );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //[Authorize(OrganizationUnitPermissions.OrganizationUnitLookup.Default)]
        public virtual async Task<ListResultDto<OrganizationUnitDto>> GetChildrenAsync(GetOrganizationUnitsInput input)
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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        //[Authorize(OrganizationUnitPermissions.OrganizationUnitLookup.Default)]
        public async Task<ListResultDto<OrganizationUnitDto>> GetParentsAsync(Guid id)
        {
            var ou = await OrganizationUnitRepository.GetAsync(id);
            var result = new List<OrganizationUnit>();
            if (!ou.Code.Contains('.'))
            {
                //If there is no parent node, no query will be performed
            }
            else
            {
                string rootCode = ou.Code.Substring(0, ou.Code.IndexOf('.'));
                var list = await OrganizationUnitRepository.GetAllChildrenWithParentCodeAsync(rootCode, null, false);
                result = list.Where(cou => ou.Code.StartsWith(cou.Code)).ToList();
            }

            var dto = ObjectMapper.Map<List<OrganizationUnit>, List<OrganizationUnitDto>>(result);
            var tree = BuildOrganizationUnitsTree(dto);
            return new ListResultDto<OrganizationUnitDto>(
                tree
                );
        }


        protected async Task<List<OrganizationUnit>> GetAllListAsync()
        {
            return await OrganizationUnitRepository.GetListAsync(includeDetails: false);
        }

        protected void AddChildren(OrganizationUnitDto parent, List<OrganizationUnitDto> list)
        {
            var children = list.Where(p => p.ParentId == parent.Id).OrderBy(ou=>ou.Sort).ThenBy(ou=>ou.Code).ToList();
            if (children.Any())
            {
                foreach (var ou in children)
                {
                    parent.AddChild(ou);
                    AddChildren(ou, list);
                }
            }
        }

        /// <summary>
        /// Build organization unit tree
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        protected List<OrganizationUnitDto> BuildOrganizationUnitsTree(List<OrganizationUnitDto> list)
        {
            var tree = new List<OrganizationUnitDto>();
            tree.AddRange(list.Where(p => !p.ParentId.HasValue).ToList());
            foreach (var ou in tree)
            {
                AddChildren(ou, list);
            }

            return tree.OrderBy(ou => ou.Sort)
                .ThenBy(ou => ou.Code)
                .ToList();
        }
    }
}
