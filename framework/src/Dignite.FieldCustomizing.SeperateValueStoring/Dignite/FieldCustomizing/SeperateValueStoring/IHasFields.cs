using System;
using System.Collections.Generic;

namespace Dignite.FieldCustomizing.SeperateValueStoring
{
    public interface IHasFields<TField> where TField : IFieldValue
    {
        Guid Id { get; }

        /// <summary>
        /// 字段值集合
        /// </summary>
        List<TField> Fields { get; }
    }
}
