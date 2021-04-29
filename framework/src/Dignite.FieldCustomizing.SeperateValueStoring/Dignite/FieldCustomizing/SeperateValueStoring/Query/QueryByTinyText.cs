
using Dignite.FieldCustomizing.SeperateValueStoring.QueryConditions;
using System;
using System.Linq;

namespace Dignite.FieldCustomizing.SeperateValueStoring.Query
{
    public class QueryByTinyText<T, TField> : IQuery<T, TField>
        where T : IHasFields<TField>
        where TField : IFieldValue
    {
        public IQueryable<T> Queryable(IQueryable<T> query, IQueryCondition queryCondition)
        {
            var _queryCondition = (TinyTextTypeQueryCondition)queryCondition;

            if (_queryCondition.Values==null || !_queryCondition.Values.Any())
            {
                return query;
            }
            else
            {
                if (_queryCondition.Values.Count() == 1)
                {
                    var value = _queryCondition.Values[0];
                    switch(_queryCondition.MatchingMode)
                    {
                        case TinyTextMatchingMode.Contain:
                            return query.Where(m =>
                                    m.Fields.Any(f =>
                                        f.FieldId == _queryCondition.FieldId
                                        && f.TinyTextValue.Contains(value)
                                    ));
                        case TinyTextMatchingMode.Equal:
                            return query.Where(m =>
                                    m.Fields.Any(f =>
                                        f.FieldId == _queryCondition.FieldId
                                        && f.TinyTextValue == value
                                    ));
                        case TinyTextMatchingMode.Prefix:
                            return query.Where(m =>
                                    m.Fields.Any(f =>
                                        f.FieldId == _queryCondition.FieldId
                                        && f.TinyTextValue.StartsWith(value)
                                    ));
                        case TinyTextMatchingMode.Suffix:
                            return query.Where(m =>
                                    m.Fields.Any(f =>
                                        f.FieldId == _queryCondition.FieldId
                                        && f.TinyTextValue.EndsWith(value)
                                    ));
                        default:
                            return query;
                    }
                }
                else
                {
                    switch (_queryCondition.MatchingMode)
                    {
                        case TinyTextMatchingMode.Contain:
                            return query.Where(m =>
                                m.Fields.Any(f =>
                                    f.FieldId == _queryCondition.FieldId
                                    && _queryCondition.Values.Any(v=>v.Contains(f.TinyTextValue))
                                ));
                        case TinyTextMatchingMode.Equal:
                            return query.Where(m =>
                                m.Fields.Any(f =>
                                    f.FieldId == _queryCondition.FieldId
                                    && _queryCondition.Values.Contains(f.TinyTextValue)
                                ));
                        case TinyTextMatchingMode.Prefix:
                            return query.Where(m =>
                                m.Fields.Any(f =>
                                    f.FieldId == _queryCondition.FieldId
                                    && _queryCondition.Values.Any(v => v.StartsWith(f.TinyTextValue))
                                ));
                        case TinyTextMatchingMode.Suffix:
                            return query.Where(m =>
                                m.Fields.Any(f =>
                                    f.FieldId == _queryCondition.FieldId
                                    && _queryCondition.Values.Any(v => v.EndsWith(f.TinyTextValue))
                                ));
                        default:
                            return query;
                    }
                }
            }
        }

        public IQueryCondition GetSearchOption(IQueryable<TField> query, Guid fieldId)
        {
            var values = query.Where(m => m.FieldId == fieldId)
                .Select(m => m.TinyTextValue)
                .Distinct().ToArray();

            return new TinyTextTypeQueryCondition
            {
                FieldId = fieldId,
                Values = values
            };
        }

    }
}
