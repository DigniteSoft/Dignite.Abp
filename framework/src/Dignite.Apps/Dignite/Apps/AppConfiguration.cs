using JetBrains.Annotations;
using System;
using Volo.Abp;

namespace Dignite.Apps
{
    [Serializable]
    public class AppConfiguration
    {
        public string Id { get; set; }

        public string Name { get; set; }


        public AppConfiguration()
        {
        }

        public AppConfiguration([NotNull] string id, [NotNull] string name)
        {
            Check.NotNull(name, nameof(name));

            Id = id;
            Name = name;
        }
    }
}