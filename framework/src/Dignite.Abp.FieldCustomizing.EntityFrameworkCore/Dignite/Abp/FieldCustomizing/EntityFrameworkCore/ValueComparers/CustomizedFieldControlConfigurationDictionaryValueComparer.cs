
using Dignite.Abp.FieldCustomizing.FieldControls;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;

namespace Dignite.Abp.FieldCustomizing.EntityFrameworkCore.ValueComparers
{
    public class CustomizedFieldControlConfigurationDictionaryValueComparer : ValueComparer<FieldControlConfigurationDictionary>
    {
        public CustomizedFieldControlConfigurationDictionaryValueComparer()
            : base(
                  (d1, d2) => d1.SequenceEqual(d2),
                  d => d.Aggregate(0, (k, v) => HashCode.Combine(k, v.GetHashCode())),
                  d => new FieldControlConfigurationDictionary(d))
        {
        }
    }
}
