using System;
using System.Linq;
using JetBrains.Annotations;
using Volo.Abp;

namespace Dignite.Abp.FieldCustomizing
{
    public static class CustomizableObjectMapper
    {
        /// <summary>
        /// Copies customized fields from the <paramref name="source"/> object
        /// to the <paramref name="destination"/> object.
        /// </summary>
        /// <typeparam name="TSource">Source class type</typeparam>
        /// <typeparam name="TDestination">Destination class type</typeparam>
        /// <param name="source">The source object</param>
        /// <param name="destination">The destination object</param>
        /// <param name="fields">Used to map properties</param>
        /// <param name="ignoredFields">Used to ignore some properties</param>
        public static void MapCustomizedFieldsTo<TSource, TDestination>(
            [NotNull] TSource source,
            [NotNull] TDestination destination,
            string[] fields=null,
            string[] ignoredFields = null)
            where TSource : IHasCustomizedFields
            where TDestination : IHasCustomizedFields
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(destination, nameof(destination));

            foreach (var keyValue in source.CustomizedFields)
            {
                if (ignoredFields != null &&
                    ignoredFields.Contains(keyValue.Key))
                {
                    continue;
                }

                if (fields == null)
                {
                    destination.CustomizedFields[keyValue.Key] = keyValue.Value;
                }
                else if (fields != null &&
                    fields.Contains(keyValue.Key))
                {
                    destination.CustomizedFields[keyValue.Key] = keyValue.Value;
                }
            }
        }
    }
}
