using JetBrains.Annotations;

namespace Dignite.FieldCustomizing
{
    public static class HasExtraFieldsObjectCustomizingExtensions
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
            [NotNull] this TSource source,
            [NotNull] TDestination destination,
            string[] properties = null,
            string[] ignoredProperties = null)
            where TSource : IHasExtraFields
            where TDestination : IHasExtraFields
        {
            CustomizableObjectMapper.MapExtraFieldsTo(
                source,
                destination,
                properties,
                ignoredProperties
            );
        }
    }
}
