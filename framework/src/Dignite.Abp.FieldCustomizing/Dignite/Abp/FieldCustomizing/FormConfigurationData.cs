using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Collections.Generic;
using Volo.Abp;

namespace Dignite.Abp.FieldCustomizing
{
    public class FormConfigurationData
    {
        /// <summary>
        /// The provider to be used to <see cref="IFormProvider.Name"/>
        /// </summary>
        [JsonProperty]
        public string FormProviderName { get; internal set; }

        [JsonProperty]
        [NotNull]
        public Dictionary<string, object> Properties { get; private set; }


        public FormConfigurationData()
        {
            Properties = new Dictionary<string, object>();
        }

        [CanBeNull]
        public T GetConfigurationOrDefault<T>(string name, T defaultValue = default)
        {
            return (T)GetConfigurationOrNull(name, defaultValue);
        }

        [CanBeNull]
        public object GetConfigurationOrNull(string name, object defaultValue = null)
        {
            return Properties.GetOrDefault(name) ??
                   defaultValue;
        }

        [NotNull]
        public FormConfigurationData SetConfiguration([NotNull] string name, [CanBeNull] object value)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.NotNull(value, nameof(value));

            Properties[name] = value;

            return this;
        }

        [NotNull]
        public FormConfigurationData ClearConfiguration([NotNull] string name)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            Properties.Remove(name);

            return this;
        }
    }
}
