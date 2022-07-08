using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dignite.Abp.FieldCustomizing.Fields.DataDictionary
{
    public static class DataDictionaryListExtensions
    {
        public static DataDictionary FindById(
            this IList<DataDictionary> source,Guid id)
        {
            foreach (var dd in source)
            {
                if (dd.Id == id)
                    return dd;
                else if (dd.Children != null && dd.Children.Any())
                {
                    return FindById(dd.Children, id);
                }
            }

            return null;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IReadOnlyList<DataDictionary> ToLevelList([NotNull] this IReadOnlyList<DataDictionary> source)
        {
            var result = new List<DataDictionary>();
            foreach (var ou in source)
            {
                result.Add(ou);
                FindChildren(result, ou);
            }
            return result;
        }

        private static void FindChildren(List<DataDictionary> list, DataDictionary ou)
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
