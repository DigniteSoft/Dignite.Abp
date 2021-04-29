using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Dignite.FieldCustomizing.SeperateValueStoring
{
    public abstract class FieldValueManagerBase<TEntity, TEntityField> : DomainService, IFieldValueManager<TEntity, TEntityField>
        where TEntity : IHasFields<TEntityField>
        where TEntityField : IFieldValue
    {

        public abstract Task<IReadOnlyList<FieldValueValidateResult>> ValidateAsync(IReadOnlyList<IFieldDefinition> fieldDefinitions,IReadOnlyList<TEntityField> fields);

        public virtual void SetFields(TEntity entity, IReadOnlyList<TEntityField> fields)
        {
            entity.ClearFields();
            entity.AddFields(fields);
        }
    }
}
