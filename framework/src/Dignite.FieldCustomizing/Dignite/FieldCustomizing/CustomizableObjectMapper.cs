using System;
using System.Linq;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Data;

namespace Dignite.FieldCustomizing
{
    public static class CustomizableObjectMapper
    {
        /// <summary>
        /// Copies extra properties from the <paramref name="source"/> object
        /// to the <paramref name="destination"/> object.
        /// </summary>
        /// <typeparam name="TSource">Source class type</typeparam>
        /// <typeparam name="TDestination">Destination class type</typeparam>
        /// <param name="source">The source object</param>
        /// <param name="destination">The destination object</param>
        /// <param name="properties">Used to map properties</param>
        /// <param name="ignoredProperties">Used to ignore some properties</param>
        public static void MapExtraFieldsTo<TSource, TDestination>(
            [NotNull] TSource source,
            [NotNull] TDestination destination,
            string[] properties=null,
            string[] ignoredProperties = null)
            where TSource : IHasExtraFields
            where TDestination : IHasExtraFields
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(destination, nameof(destination));

            foreach (var keyValue in source.ExtraFields)
            {
                if (ignoredProperties != null &&
                    ignoredProperties.Contains(keyValue.Key))
                {
                    continue;
                }

                if (properties == null)
                {
                    destination.ExtraFields[keyValue.Key] = keyValue.Value;
                }
                else if (properties != null &&
                    properties.Contains(keyValue.Key))
                {
                    destination.ExtraFields[keyValue.Key] = keyValue.Value;
                }
            }
        }
    }
}
