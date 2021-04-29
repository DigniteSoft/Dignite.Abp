using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Dignite.FieldCustomizing.SeperateValueStoring
{
    public interface IFieldValueManager<TEntity, TEntityField> : IDomainService
        where TEntity : IHasFields<TEntityField>
        where TEntityField : IFieldValue
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldDefinitions"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        Task<IReadOnlyList<FieldValueValidateResult>> ValidateAsync(IReadOnlyList<IFieldDefinition> fieldDefinitions, IReadOnlyList<TEntityField> fields);

        /// <summary>
        /// 设置实体的字段
        /// </summary>
        /// <param name="entity">含有字段的实体</param>
        /// <param name="fields">实体中的字段</param>
        /// <returns></returns>
        void SetFields(TEntity entity, IReadOnlyList<TEntityField> fields);
    }
}
