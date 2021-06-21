using System;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Dignite.FieldCustomizing.EntityFrameworkCore.ValueComparers
{
    public class ExtraFieldDictionaryValueComparer : ValueComparer<ExtraFieldDictionary>
    {
        public ExtraFieldDictionaryValueComparer()
            : base(
                  (d1, d2) => d1.SequenceEqual(d2),
                  d => d.Aggregate(0, (k, v) => HashCode.Combine(k, v.GetHashCode())),
                  d => new ExtraFieldDictionary(d))
        {
        }
    }
}
