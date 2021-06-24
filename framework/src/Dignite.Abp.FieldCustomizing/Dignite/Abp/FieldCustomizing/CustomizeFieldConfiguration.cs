using JetBrains.Annotations;
using System.Collections.Generic;
using Volo.Abp;

namespace Dignite.Abp.FieldCustomizing
{
    public class CustomizeFieldConfiguration
    {
        /// <summary>
        /// The provider to be used to <see cref="ICustomizeFieldProvider.Name"/>
        /// </summary>
        public string ProviderName { get; set; }


        [NotNull]
        public Dictionary<string, object> Properties { get; set; }


        public CustomizeFieldConfiguration()
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
        public CustomizeFieldConfiguration SetConfiguration([NotNull] string name, [CanBeNull] object value)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.NotNull(value, nameof(value));

            Properties[name] = value;

            return this;
        }

        [NotNull]
        public CustomizeFieldConfiguration ClearConfiguration([NotNull] string name)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            Properties.Remove(name);

            return this;
        }
    }
}
