using JetBrains.Annotations;

namespace Dignite.FieldCustomizing
{
    public interface ICustomizeFieldDefinition
    {
        [NotNull]
        string Name { get; }

        [NotNull]
        string DisplayName { get; set; }


        /// <summary>
        /// Default value of the field.
        /// </summary>
        [CanBeNull]
        public string DefaultValue { get; set; }

        [NotNull]
        CustomizeFieldConfiguration Configuration { get; }
    }
}
