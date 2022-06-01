using Dignite.Abp.FieldCustomizing.FieldControls;
using JetBrains.Annotations;

namespace Dignite.Abp.FieldCustomizing
{
    public interface ICustomizeFieldDefinition
    {
        [NotNull]
        string Name { get; set; }

        [NotNull]
        string DisplayName { get; set; }


        /// <summary>
        /// Default value of the field.
        /// </summary>
        [CanBeNull]
        public string DefaultValue { get; set; }

        [NotNull]
        public string FieldControlProviderName { get; set; }

        [NotNull]
        FieldControlConfigurationDictionary Configuration { get; set; }
    }
}
