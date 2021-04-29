using Volo.Abp.Domain.Services;

namespace Dignite.FieldCustomizing.SeperateValueStoring.Query
{
    /// <summary>
    /// 
    /// </summary>
    public interface IQueryFactory:IDomainService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="valueType"></param>
        /// <returns></returns>
        IQuery<T, TField> Create<T, TField>(FieldValueType valueType)
            where T : IHasFields<TField>
            where TField : IFieldValue;

    }
}
