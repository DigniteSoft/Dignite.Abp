using System;

namespace Dignite.FieldCustomizing.SeperateValueStoring.QueryConditions
{
    public class LongTextTypeQueryCondition : IQueryCondition
    {

        public Guid FieldId { get; set; }

        public FieldValueType ValueType => FieldValueType.LongText;

        /// <summary>
        /// <see cref="IFieldValue.LongTextValue"/> 不何为空
        /// </summary>
        public bool NotEmpty { get; set; }

        /// <summary>
        /// <see cref="IFieldValue.LongTextValue"/> 包含的文本
        /// </summary>
        public string ContainText { get; set; }
    }
}
