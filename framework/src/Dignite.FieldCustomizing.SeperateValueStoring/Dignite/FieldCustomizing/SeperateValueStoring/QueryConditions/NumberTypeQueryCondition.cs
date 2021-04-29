using System;

namespace Dignite.FieldCustomizing.SeperateValueStoring.QueryConditions
{
    public class NumberTypeQueryCondition : IQueryCondition
    {
        public Guid FieldId { get; set; }
        public FieldValueType ValueType => FieldValueType.Number;

        /// <summary>
        /// 最小值
        /// </summary>
        public double? MinimumValue { get; set; }

        /// <summary>
        /// 最大值
        /// </summary>
        public double? MaximumValue { get; set; }
    }
}
