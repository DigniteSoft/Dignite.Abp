using JetBrains.Annotations;
using System.Collections.Generic;
using Volo.Abp;

namespace Dignite.FieldCustomizing
{
    public class FieldConfiguration
    {
        /// <summary>
        /// The provider to be used to <see cref="IFieldProvider.Name"/>
        /// </summary>
        public string ProviderName { get; set; }


        [NotNull]
        public Dictionary<string, object> Properties { get; set; }


        public FieldConfiguration()
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
        public FieldConfiguration SetConfiguration([NotNull] string name, [CanBeNull] object value)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.NotNull(value, nameof(value));

            Properties[name] = value;

            return this;
        }

        [NotNull]
        public FieldConfiguration ClearConfiguration([NotNull] string name)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            Properties.Remove(name);

            return this;
        }
    }
}
