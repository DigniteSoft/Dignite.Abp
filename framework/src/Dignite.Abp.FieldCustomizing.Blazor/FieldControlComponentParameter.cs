using Dignite.Abp.FieldCustomizing.FieldControls;

namespace Dignite.Abp.FieldCustomizing.Blazor
{
    public class FieldControlComponentParameter
    {
        public FieldControlComponentParameter(string name, string displayName, string fieldControlProviderName, string value, FieldControlConfigurationDictionary configuration)
        {
            Name = name;
            DisplayName = displayName;
            FieldControlProviderName = fieldControlProviderName;
            Value = value;
            Configuration = configuration;
        }

        public string Name { get; set; }

        public string DisplayName { get; set; }


        /// <summary>
        /// The provider to be used to <see cref="IFieldControlProvider.Name"/>
        /// </summary>
        public string FieldControlProviderName { get; set; }


        /// <summary>
        /// Default value of the field.
        /// </summary>
        public object Value { get; set; }

        public FieldControlConfigurationDictionary Configuration { get; set; }
    }
}
