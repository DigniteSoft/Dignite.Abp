using System;

namespace Dignite.FieldCustomizing.SeperateValueStoring.QueryConditions
{
    public class DateTimeTypeQueryCondition : IQueryCondition
    {
        public Guid FieldId { get; set; }
        public FieldValueType ValueType => FieldValueType.DateTime;

        /// <summary>
        /// 最小值
        /// </summary>
        public DateTime? MinimumValue { get; set; }

        /// <summary>
        /// 最大值
        /// </summary>
        public DateTime? MaximumValue { get; set; }
    }
}
