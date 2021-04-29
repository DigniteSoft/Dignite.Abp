using System;

namespace Dignite.FieldCustomizing.SeperateValueStoring.QueryConditions
{
    public class BooleanTypeQueryCondition : IQueryCondition
    {
        public Guid FieldId { get; set; }

        public FieldValueType ValueType => FieldValueType.Boolean;

        public bool? Lightswitch { get; set; }
    }
}
