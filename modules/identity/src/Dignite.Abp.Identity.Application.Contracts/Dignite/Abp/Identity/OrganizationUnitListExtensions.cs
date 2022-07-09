using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dignite.Abp.Identity
{
    public static class OrganizationUnitListExtensions
    {
        /// <summary>
        /// 移动组织机构
        /// </summary>
        /// <param name="source"></param>
        /// <param name="ou"></param>
        /// <param name="targetParent">目标父组织机构</param>
        /// <param name="index">移动后的索引</param>
        public static void Move([NotNull] this IList<OrganizationUnitDto> source, OrganizationUnitDto ou, OrganizationUnitDto targetParent, int index)
        {
            if (ou.ParentId.HasValue)
            {
                source.FindById(ou.ParentId.Value).Remove(ou);
            }
            else
            {
                source.RemoveAll(i=>i.Id==ou.Id);
            }


            if (targetParent!=null)
            {
                targetParent.InsertChild(index, ou);
            }
            else
            {
                ou.ParentId = null;
                source.Insert(index, ou);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static OrganizationUnitDto FindById([NotNull] this IEnumerable<OrganizationUnitDto> source,Guid id)
        {
            OrganizationUnitDto result = source.FirstOrDefault(ou => ou.Id == id);

            if (result == null)
            {
                foreach (var item in source)
                {
                    if (item.Children != null && item.Children.Any())
                    {
                        result = FindById(item.Children, id);
                        if (result != null)
                            return result;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IReadOnlyList<OrganizationUnitDto> ToLevelList([NotNull] this IReadOnlyList<OrganizationUnitDto> source)
        {
            var result = new List<OrganizationUnitDto>();
            foreach (var ou in source)
            {
                result.Add(ou);
                FindChildren(result, ou);
            }
            return result;
        }

        private static void FindChildren(List<OrganizationUnitDto> list, OrganizationUnitDto ou)
        {
            if (ou.Children != null && ou.Children.Any())
            {
                foreach (var c in ou.Children)
                {
                    list.Add(c);
                    FindChildren(list, c);
                }
            }
        }
    }
}
