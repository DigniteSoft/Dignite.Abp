using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Dignite.FieldCustomizing.SeperateValueStoring.QueryConditions
{
    public class QueryConditionJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(IQueryCondition).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader,
            Type objectType, object existingValue, JsonSerializer serializer)
        {

            var jsonObject = JObject.Load(reader);
            var target = Create(objectType, jsonObject);
            serializer.Populate(jsonObject.CreateReader(), target);
            return target;
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer,
            object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        private IQueryCondition Create(Type objectType, JObject jsonObject)
        {
            var valueType = jsonObject["valueType"].ToString().ToEnum<FieldValueType>(true);

            switch (valueType)
            {
                case FieldValueType.Boolean:
                    return new BooleanTypeQueryCondition();
                case FieldValueType.DateTime:
                    return new DateTimeTypeQueryCondition();
                case FieldValueType.Guid:
                    return new GuidTypeQueryCondition();
                case FieldValueType.TinyText:
                    return new TinyTextTypeQueryCondition();
                case FieldValueType.LongText:
                    return new LongTextTypeQueryCondition();
                case FieldValueType.Number:
                    return new NumberTypeQueryCondition();
                default:
                    return new LongTextTypeQueryCondition();
            }
        }
    }
}
