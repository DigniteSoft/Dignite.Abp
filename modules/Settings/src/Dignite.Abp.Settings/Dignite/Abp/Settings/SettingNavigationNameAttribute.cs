using System;
using System.Reflection;
using JetBrains.Annotations;
using Volo.Abp;

namespace Dignite.Abp.Settings
{
    public class SettingNavigationNameAttribute: Attribute
    {
        [NotNull]
        public string Name { get; }

        public SettingNavigationNameAttribute([NotNull] string name)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            Name = name;
        }

        public virtual string GetName(Type type)
        {
            return Name;
        }

        public static string GetNavigationName<T>()
        {
            return GetNavigationName(typeof(T));
        }

        public static string GetNavigationName(Type type)
        {
            var nameAttribute = type.GetCustomAttribute<SettingNavigationNameAttribute>();

            if (nameAttribute == null)
            {
                return type.FullName;
            }

            return nameAttribute.GetName(type);
        }
    }
}