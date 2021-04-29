using System;

namespace Dignite.FieldCustomizing.SeperateValueStoring.QueryConditions
{
    public class GuidTypeQueryCondition : IQueryCondition
    {
        public Guid FieldId { get; set; }
        public FieldValueType ValueType => FieldValueType.Guid;
        public Guid[] Values { get; set; }
    }
}
