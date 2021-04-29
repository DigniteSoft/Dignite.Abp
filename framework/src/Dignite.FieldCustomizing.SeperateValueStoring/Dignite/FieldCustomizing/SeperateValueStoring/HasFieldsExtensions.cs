using System.Collections.Generic;

namespace Dignite.FieldCustomizing.SeperateValueStoring
{
    /// <summary>
    /// 含有字段的实体针对字段值集合操作的扩展
    /// </summary>
    public static class HasFieldsExtensions
    {
        /// <summary>
        /// 添加字段值集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="fields"></param>

        public static void AddFields<T> (this IHasFields<T> entity, IReadOnlyList<T> fields) 
            where T:IFieldValue
        {
            foreach (var fv in fields)
            {
                fv.ForeignId = entity.Id;
                entity.Fields.Add(fv);
            }
        }



        /// <summary>
        /// 移除字段值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="fields"></param>
        public static void ClearFields<T>(this IHasFields<T> entity)
            where T : IFieldValue
        {
            foreach (var fv in entity.Fields)
            {
                entity.Fields.Remove(fv);
            }
        }
    }
}
