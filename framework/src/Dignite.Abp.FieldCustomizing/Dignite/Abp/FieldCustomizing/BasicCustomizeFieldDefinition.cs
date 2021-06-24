using JetBrains.Annotations;
using System;

namespace Dignite.Abp.FieldCustomizing
{
    [Serializable]
    public class BasicCustomizeFieldDefinition : ICustomizeFieldDefinition
    {

        public BasicCustomizeFieldDefinition(string name, string displayName, string defaultValue, CustomizeFieldConfiguration configuration)
        {
            Name = name;
            DisplayName = displayName;
            DefaultValue = defaultValue;
            Configuration = configuration;
        }

        [NotNull]
        public string Name { get; set; }

        [NotNull]
        public string DisplayName { get; set; }


        /// <summary>
        /// Default value of the field.
        /// </summary>
        [CanBeNull]
        public string DefaultValue { get; set; }

        [NotNull]
        public CustomizeFieldConfiguration Configuration { get; set; }
    }
}
