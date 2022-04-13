using System;

namespace Dignite.Abp.FileManagement
{
    public class BlobEntityResolveContext : IBlobEntityResolveContext
    {
        public IServiceProvider ServiceProvider { get; }

        public string EntityType { get; set; }

        public string EntityId { get; set; }

        public bool HasResolvedTenantOrHost()
        {
            return (EntityType != null && EntityId!=null);
        }

        public BlobEntityResolveContext(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
    }
}