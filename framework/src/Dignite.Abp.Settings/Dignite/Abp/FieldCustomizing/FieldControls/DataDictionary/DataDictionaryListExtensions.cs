using System;
using System.Collections.Generic;
using System.Linq;

namespace Dignite.Abp.FieldCustomizing.FieldControls.DataDictionary
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
    }
}
