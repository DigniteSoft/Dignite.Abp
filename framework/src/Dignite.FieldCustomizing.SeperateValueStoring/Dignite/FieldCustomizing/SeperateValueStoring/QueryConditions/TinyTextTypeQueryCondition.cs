using System;

namespace Dignite.FieldCustomizing.SeperateValueStoring.QueryConditions
{
    public class TinyTextTypeQueryCondition : IQueryCondition
    {
        public Guid FieldId { get; set; }

        public FieldValueType ValueType => FieldValueType.TinyText;

        public string[] Values { get; set; }

        public TinyTextMatchingMode MatchingMode { get; set; }
    }

    /// <summary>
    /// 数据比较方式
    /// </summary>
    public enum TinyTextMatchingMode
    {
        /// <summary>
        /// 相等
        /// </summary>
        Equal,
        /// <summary>
        /// 前缀
        /// </summary>
        Prefix,
        /// <summary>
        /// 后缀
        /// </summary>
        Suffix,
        /// <summary>
        /// 包含
        /// </summary>
        Contain
    }
}
