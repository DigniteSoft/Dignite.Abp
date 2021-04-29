using Newtonsoft.Json;
using System;

namespace Dignite.FieldCustomizing.SeperateValueStoring.QueryConditions
{
    /// <summary>
    /// 查询条件的接口
    /// </summary>
    [JsonConverter(typeof(QueryConditionJsonConverter))]
    public interface IQueryCondition
    {
        public Guid FieldId { get; set; }

        public FieldValueType ValueType { get; }
    }
}
