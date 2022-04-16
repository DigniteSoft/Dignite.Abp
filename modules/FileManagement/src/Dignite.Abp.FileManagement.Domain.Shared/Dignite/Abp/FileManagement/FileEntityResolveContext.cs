using System;

namespace Dignite.Abp.FileManagement
{
    public class FileEntityResolveContext : IFileEntityResolveContext
    {
        public IServiceProvider ServiceProvider { get; }

        public string EntityType { get; set; }

        public string EntityId { get; set; }

        public bool HasResolvedTenantOrHost()
        {
            return (EntityType != null && EntityId!=null);
        }

        public FileEntityResolveContext(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
    }
}