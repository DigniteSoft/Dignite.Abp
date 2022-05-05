using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dignite.Abp.Identity
{
    public static class OrganizationUnitListExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static OrganizationUnitDto FindById([NotNull] this IList<OrganizationUnitDto> source,Guid id)
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
                AddChildren(result, ou);
            }
            return result;
        }

        private static void AddChildren(List<OrganizationUnitDto> list, OrganizationUnitDto ou)
        {
            if (ou.Children != null && ou.Children.Any())
            {
                foreach (var c in ou.Children)
                {
                    list.Add(c);
                    AddChildren(list, ou);
                }
            }
        }
    }
}
