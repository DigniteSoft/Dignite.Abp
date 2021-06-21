using JetBrains.Annotations;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.BlobStoringManagement
{
    public interface IBlobEntityResolveContext : IServiceProviderAccessor
    {
        [CanBeNull]
        string EntityType { get; set; }

        [CanBeNull]
        string EntityId { get; set; }

    }
}