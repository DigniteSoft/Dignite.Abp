using Dignite.FieldCustomizing.SeperateValueStoring.QueryConditions;
using System;
using System.Linq;

namespace Dignite.FieldCustomizing.SeperateValueStoring.Query
{
    public class QueryByGuid<T, TField> : IQuery<T, TField>
        where T : IHasFields<TField>
        where TField : IFieldValue
    {

        public IQueryable<T> Queryable(IQueryable<T> query, IQueryCondition queryCondition)
        {
            var _queryCondition= (GuidTypeQueryCondition)queryCondition;

            if (_queryCondition.Values==null|| !_queryCondition.Values.Any())
            {
                return query;
            }
            else
            {
                return query.Where(m =>
                    m.Fields.Any(f => f.FieldId == _queryCondition.FieldId
                        && _queryCondition.Values.Contains(f.GuidValue.Value)
                    ));
            }
        }

        public IQueryCondition GetSearchOption(IQueryable<TField> query, Guid fieldId)
        {
            var values = query.Where(m => m.FieldId == fieldId && m.GuidValue.HasValue)
                .Select(m => m.GuidValue.Value)
                .Distinct().ToArray();

            return new GuidTypeQueryCondition
            { 
                FieldId = fieldId, 
                Values = values
            };
        }
    }
} 
