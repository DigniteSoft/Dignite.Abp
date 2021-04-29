using Volo.Abp.Domain.Services;

namespace Dignite.FieldCustomizing.SeperateValueStoring.Query
{
    public class QueryFactory : DomainService, IQueryFactory
    {

        public IQuery<T, TField> Create<T, TField>(FieldValueType valueType)
            where T : IHasFields<TField>
            where TField : IFieldValue
        {
            IQuery<T, TField> query;

            switch (valueType)
            {
                case FieldValueType.Boolean:
                    query= new QueryByBoolean<T,TField>();
                    break;
                case FieldValueType.DateTime:
                    query= new QueryByDateTime<T, TField>();
                    break;
                case FieldValueType.Guid:
                    query = new QueryByGuid<T, TField>();
                    break;
                case FieldValueType.TinyText:
                    query = new QueryByTinyText<T, TField>();
                    break;
                case FieldValueType.Number:
                    query = new QueryByNumber<T, TField>();
                    break;
                default:
                    query = new QueryByLongText<T, TField>();
                    break;
            }

            return query;
        }      
    }
}
