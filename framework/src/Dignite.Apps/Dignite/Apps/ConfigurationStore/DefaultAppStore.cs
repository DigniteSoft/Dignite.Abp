using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;

namespace Dignite.Apps.ConfigurationStore
{
    [Dependency(TryRegister = true)]
    public class DefaultAppStore : IAppStore, ITransientDependency
    {
        private readonly DigniteDefaultAppStoreOptions _options;

        public DefaultAppStore(IOptionsSnapshot<DigniteDefaultAppStoreOptions> options)
        {
            _options = options.Value;
        }

        public Task<IReadOnlyList<AppConfiguration>> GetListAsync()
        {
            return Task.FromResult(GetList());
        }

        public Task<AppConfiguration> FindAsync(string id)
        {
            return Task.FromResult(Find(id));
        }

        public IReadOnlyList<AppConfiguration> GetList()
        {
            return _options.Apps;
        }

        public AppConfiguration Find(string id)
        {
            return _options.Apps?.FirstOrDefault(t => t.Id == id);
        }

    }
}