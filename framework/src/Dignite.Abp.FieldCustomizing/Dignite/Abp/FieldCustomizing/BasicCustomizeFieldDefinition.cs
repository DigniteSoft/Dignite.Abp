using Dignite.Abp.FieldCustomizing.FieldControls;
using JetBrains.Annotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace Dignite.Abp.FieldCustomizing
{
    [Serializable]
    public class BasicCustomizeFieldDefinition
    {

        public BasicCustomizeFieldDefinition(string name, string displayName, string fieldControlProviderName, string defaultValue, FieldControlConfigurationDictionary configuration)
        {
            Name = name;
            DisplayName = displayName;
            FieldControlProviderName=fieldControlProviderName;
            DefaultValue = defaultValue;
            Configuration = configuration;
        }

        [Required]
        [NotNull]
        [StringLength(BasicCustomizeFieldDefinitionConsts.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [NotNull]
        [StringLength(BasicCustomizeFieldDefinitionConsts.MaxDisplayNameLength)]
        public string DisplayName { get; set; }


        /// <summary>
        /// The provider to be used to <see cref="IFieldControlProvider.Name"/>
        /// </summary>
        [Required]
        [StringLength(BasicCustomizeFieldDefinitionConsts.MaxFieldControlProviderNameLength)]
        public string FieldControlProviderName { get; set; }


        /// <summary>
        /// Default value of the field.
        /// </summary>
        [CanBeNull]
        public string DefaultValue { get; set; }

        [NotNull]
        public FieldControlConfigurationDictionary Configuration { get; set; }
    }
}
