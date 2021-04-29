
using Dignite.FieldCustomizing.SeperateValueStoring.QueryConditions;
using System;
using System.Linq;

namespace Dignite.FieldCustomizing.SeperateValueStoring.Query
{
    public class QueryByLongText<T, TField> : IQuery<T, TField>
        where T : IHasFields<TField>
        where TField : IFieldValue
    {

        public IQueryable<T> Queryable(IQueryable<T> query, IQueryCondition queryCondition)
        {
            var _queryCondition = (LongTextTypeQueryCondition)queryCondition;

            if (_queryCondition.NotEmpty)
            {
                return query.Where(m =>
                    m.Fields.Any(f =>
                        f.FieldId == _queryCondition.FieldId
                    ));
            }
            if (string.IsNullOrWhiteSpace(_queryCondition.ContainText))
            {
                return query;
            }
            else
            {
                return query.Where(m =>
                    m.Fields.Any(f =>
                        f.FieldId == _queryCondition.FieldId
                        && f.LongTextValue.Contains(_queryCondition.ContainText)
                    ));
            }
        }

        public IQueryCondition GetSearchOption(IQueryable<TField> query, Guid fieldId)
        {
            return null;
        }
    }
}
