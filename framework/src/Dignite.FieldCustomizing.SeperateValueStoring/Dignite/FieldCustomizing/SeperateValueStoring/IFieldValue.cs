using JetBrains.Annotations;
using System;

namespace Dignite.FieldCustomizing.SeperateValueStoring
{
    /// <summary>
    /// 字段值基类
    /// </summary>
    public interface IFieldValue
    {
        /// <summary>
        /// 外键Id
        /// </summary>
        [NotNull]
        Guid ForeignId { get; set; }


        /// <summary>
        /// 字段Id
        /// </summary>
        [NotNull]
        Guid FieldId { get; set; }

        /// <summary>
        /// 用于存储不超过<see cref="FieldValueConsts.MaxTinyTextLength"/>个字符的文本;
        /// 支持数据库索引；
        /// </summary>
        [CanBeNull]
        string TinyTextValue { get; set; }

        /// <summary>
        /// 用于存储数字类型值;
        /// 支持数据库索引；
        /// </summary>
        double? NumberValue { get; set; }

        /// <summary>
        /// 用于存储数字类型值
        /// 支持数据库索引；
        /// </summary>
        DateTime? DateTimeValue { get; set; }

        /// <summary>
        /// 用于存储布尔型值
        /// </summary>
        bool? BooleanValue { get;  set; }

        /// <summary>
        /// 用于存储Guid类型值
        /// 支持数据库索引；
        /// </summary>
        Guid? GuidValue { get;  set; }


        /// <summary>
        /// 用于存储任意类型转换为字符串的值；比如html\json等;
        /// 不支持数据库索引；
        /// </summary>
        [CanBeNull]
        string LongTextValue { get;  set; }


    }
}
