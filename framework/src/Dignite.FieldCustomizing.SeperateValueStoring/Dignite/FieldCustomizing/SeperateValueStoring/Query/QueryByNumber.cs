
using Dignite.FieldCustomizing.SeperateValueStoring.QueryConditions;
using System;
using System.Linq;

namespace Dignite.FieldCustomizing.SeperateValueStoring.Query
{
    public class QueryByNumber<T, TField> : IQuery<T, TField>
        where T : IHasFields<TField>
        where TField : IFieldValue
    {

        public IQueryable<T> Queryable(IQueryable<T> query, IQueryCondition queryCondition)
        {
            var _queryCondition = (NumberTypeQueryCondition)queryCondition;

            if (!_queryCondition.MinimumValue.HasValue && !_queryCondition.MaximumValue.HasValue)
            {
                return query;
            }
            else if (_queryCondition.MinimumValue.HasValue && !_queryCondition.MaximumValue.HasValue)
            {
                return query.Where(m =>
                    m.Fields.Any(f =>
                        f.FieldId == _queryCondition.FieldId
                        && f.NumberValue.Value >= _queryCondition.MinimumValue.Value
                    ));
            }
            else if (!_queryCondition.MinimumValue.HasValue && _queryCondition.MaximumValue.HasValue)
            {
                return query.Where(m =>
                    m.Fields.Any(f =>
                        f.FieldId == _queryCondition.FieldId
                        && f.NumberValue.Value <= _queryCondition.MaximumValue.Value
                    ));
            }
            else
            {
            return query.Where(m =>
                m.Fields.Any(f =>
                    f.FieldId == _queryCondition.FieldId
                    && f.NumberValue.Value >= _queryCondition.MinimumValue.Value
                    && f.NumberValue.Value <= _queryCondition.MaximumValue.Value
                ));
            }
        }

        public IQueryCondition GetSearchOption(IQueryable<TField> query, Guid fieldId)
        {
            var condition = new NumberTypeQueryCondition() {
                FieldId = fieldId
            };
            condition.MinimumValue = query.Where(m => m.FieldId == fieldId).Min(m => m.NumberValue);
            condition.MaximumValue = query.Where(m => m.FieldId == fieldId).Max(m => m.NumberValue);

            return condition;
        }
    }
}
