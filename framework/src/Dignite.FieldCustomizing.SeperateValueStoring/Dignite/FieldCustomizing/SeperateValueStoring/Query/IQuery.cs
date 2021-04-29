using Dignite.FieldCustomizing.SeperateValueStoring.QueryConditions;
using System;
using System.Linq;

namespace Dignite.FieldCustomizing.SeperateValueStoring.Query
{
    /// <summary>
    /// 通过字段查询记录
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TField"></typeparam>
    public interface IQuery<T,TField>
        where T: IHasFields<TField>
        where TField : IFieldValue
    {

        IQueryable<T> Queryable(IQueryable<T> query, IQueryCondition queryCondition);


        IQueryCondition GetSearchOption(IQueryable<TField> query, Guid fieldId);
    }
}
