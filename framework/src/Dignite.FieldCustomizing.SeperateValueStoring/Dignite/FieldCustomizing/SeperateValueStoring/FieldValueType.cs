
namespace Dignite.FieldCustomizing.SeperateValueStoring
{
    public enum FieldValueType
    {
        /// <summary>
        /// 用于短文本，数据表索引该类型的数据
        /// </summary>
        TinyText,

        /// <summary>
        /// 数字型
        /// </summary>
        Number,

        /// <summary>
        /// 日期型
        /// </summary>
        DateTime,

        /// <summary>
        /// 布尔型
        /// </summary>
        Boolean,

        /// <summary>
        /// Guid类型
        /// </summary>
        Guid,

        /// <summary>
        /// 长文本，数据表不索引该类型数据
        /// </summary>
        LongText
    }
}
