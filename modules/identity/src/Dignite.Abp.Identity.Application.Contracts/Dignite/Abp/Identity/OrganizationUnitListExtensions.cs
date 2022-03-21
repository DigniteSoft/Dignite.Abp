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

    }
}
