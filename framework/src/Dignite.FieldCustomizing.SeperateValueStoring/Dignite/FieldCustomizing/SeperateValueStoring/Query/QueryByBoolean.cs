using Dignite.FieldCustomizing.SeperateValueStoring.QueryConditions;
using System;
using System.Linq;

namespace Dignite.FieldCustomizing.SeperateValueStoring.Query
{
    public class QueryByBoolean<T, TField> : IQuery<T, TField>
        where T : IHasFields<TField>
        where TField : IFieldValue
    {

        public IQueryable<T> Queryable(IQueryable<T> query, IQueryCondition queryCondition)
        {
            var _queryCondition = (BooleanTypeQueryCondition)queryCondition;

            if (!_queryCondition.Lightswitch.HasValue)
            {
                return query;
            }
            else 
            {
                return query.Where(m =>
                    m.Fields.Any(f =>
                        f.FieldId == _queryCondition.FieldId
                        && f.BooleanValue.Value == _queryCondition.Lightswitch.Value
                    ));
            }
        }
        public IQueryCondition GetSearchOption(IQueryable<TField> query, Guid fieldId)
        {
            return null;
        }
    }
}
